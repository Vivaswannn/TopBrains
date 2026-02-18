using System;
using System.Collections.Generic;
using System.Linq;

namespace GenericsScenario3_Hospital
{
    public interface IPatient
    {
        int PatientId { get; }
        string Name { get; }
        DateTime DateOfBirth { get; }
        BloodType BloodType { get; }
    }

    public enum BloodType { A, B, AB, O }

    public class PriorityQueue<T> where T : IPatient
    {
        private readonly SortedDictionary<int, Queue<T>> _queues = new();

        public void Enqueue(T patient, int priority)
        {
            if (priority < 1 || priority > 5)
                throw new ArgumentOutOfRangeException(nameof(priority), "Priority must be 1-5.");
            if (!_queues.ContainsKey(priority))
                _queues[priority] = new Queue<T>();
            _queues[priority].Enqueue(patient);
        }

        public T Dequeue()
        {
            var key = _queues.Keys.FirstOrDefault(k => _queues[k].Count > 0);
            if (key == 0)
                throw new InvalidOperationException("Queue is empty.");
            var patient = _queues[key].Dequeue();
            return patient;
        }

        public T Peek()
        {
            var key = _queues.Keys.FirstOrDefault(k => _queues[k].Count > 0);
            if (key == 0)
                throw new InvalidOperationException("Queue is empty.");
            return _queues[key].Peek();
        }

        public int GetCountByPriority(int priority)
        {
            return _queues.TryGetValue(priority, out var q) ? q.Count : 0;
        }
    }

    public class MedicalRecord<T> where T : IPatient
    {
        private readonly T _patient;
        private readonly List<string> _diagnoses = new();
        private readonly Dictionary<DateTime, string> _treatments = new();

        public MedicalRecord(T patient)
        {
            _patient = patient;
        }

        public void AddDiagnosis(string diagnosis, DateTime date)
        {
            _diagnoses.Add($"{date:yyyy-MM-dd}: {diagnosis}");
        }

        public void AddTreatment(string treatment, DateTime date)
        {
            _treatments[date] = treatment;
        }

        public IEnumerable<KeyValuePair<DateTime, string>> GetTreatmentHistory()
        {
            return _treatments.OrderBy(x => x.Key);
        }
    }

    public class PediatricPatient : IPatient
    {
        public int PatientId { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public BloodType BloodType { get; set; }
        public string GuardianName { get; set; }
        public double Weight { get; set; }
    }

    public class GeriatricPatient : IPatient
    {
        public int PatientId { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public BloodType BloodType { get; set; }
        public List<string> ChronicConditions { get; } = new();
        public int MobilityScore { get; set; }
    }

    public class MedicationSystem<T> where T : IPatient
    {
        private readonly Dictionary<int, List<(string medication, DateTime time)>> _medications = new();

        public void PrescribeMedication(T patient, string medication, Func<T, bool> dosageValidator)
        {
            if (!dosageValidator(patient))
                throw new InvalidOperationException("Dosage validation failed for patient.");
            if (!_medications.ContainsKey(patient.PatientId))
                _medications[patient.PatientId] = new List<(string, DateTime)>();
            _medications[patient.PatientId].Add((medication, DateTime.Now));
        }

        public bool CheckInteractions(T patient, string newMedication)
        {
            if (!_medications.TryGetValue(patient.PatientId, out var list))
                return false;
            var interactions = new[] { "Warfarin", "Aspirin", "Ibuprofen" };
            if (!interactions.Contains(newMedication)) return false;
            return list.Any(m => interactions.Contains(m.medication) && m.medication != newMedication);
        }
    }

    class Program
    {
        static void Main()
        {
            var queue = new PriorityQueue<IPatient>();
            var p1 = new PediatricPatient { PatientId = 1, Name = "Child1", DateOfBirth = DateTime.Now.AddYears(-5), BloodType = BloodType.O, Weight = 18 };
            var p2 = new PediatricPatient { PatientId = 2, Name = "Child2", DateOfBirth = DateTime.Now.AddYears(-8), BloodType = BloodType.A, Weight = 25 };
            var g1 = new GeriatricPatient { PatientId = 3, Name = "Elder1", DateOfBirth = DateTime.Now.AddYears(-75), BloodType = BloodType.B, MobilityScore = 6 };
            var g2 = new GeriatricPatient { PatientId = 4, Name = "Elder2", DateOfBirth = DateTime.Now.AddYears(-80), BloodType = BloodType.AB, MobilityScore = 4 };

            queue.Enqueue(p1, 2);
            queue.Enqueue(p2, 1);
            queue.Enqueue(g1, 3);
            queue.Enqueue(g2, 2);

            Console.WriteLine("Count priority 1: " + queue.GetCountByPriority(1));
            Console.WriteLine("Peek: " + queue.Peek().Name);
            Console.WriteLine("Dequeue: " + queue.Dequeue().Name);

            var record = new MedicalRecord<PediatricPatient>(p1);
            record.AddDiagnosis("Flu", DateTime.Now.AddDays(-1));
            record.AddTreatment("Rest", DateTime.Now);
            foreach (var t in record.GetTreatmentHistory())
                Console.WriteLine($"Treatment: {t.Key:d} - {t.Value}");

            var meds = new MedicationSystem<PediatricPatient>();
            meds.PrescribeMedication(p1, "Paracetamol", pt => pt.Weight >= 10 && pt.Weight <= 50);
            Console.WriteLine("Check interaction: " + meds.CheckInteractions(p1, "Warfarin"));
        }
    }
}
