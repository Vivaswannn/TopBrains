using System;
using System.Collections.Generic;
using System.Linq;

namespace CollectionsChallenge1_Hospital
{
    public class Patient
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Condition { get; set; }
        public List<string> MedicalHistory { get; } = new List<string>();

        public Patient(int id, string name, int age, string condition)
        {
            Id = id;
            Name = name;
            Age = age;
            Condition = condition;
        }
    }

    public class HospitalManager
    {
        private readonly Dictionary<int, Patient> _patients = new Dictionary<int, Patient>();
        private readonly Queue<Patient> _appointmentQueue = new Queue<Patient>();

        public void RegisterPatient(int id, string name, int age, string condition)
        {
            var patient = new Patient(id, name, age, condition);
            _patients[id] = patient;
        }

        public void ScheduleAppointment(int patientId)
        {
            if (_patients.TryGetValue(patientId, out var patient))
                _appointmentQueue.Enqueue(patient);
        }

        public Patient ProcessNextAppointment()
        {
            return _appointmentQueue.Count > 0 ? _appointmentQueue.Dequeue() : null;
        }

        public List<Patient> FindPatientsByCondition(string condition)
        {
            return _patients.Values
                .Where(p => string.Equals(p.Condition, condition, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }
    }

    class Program
    {
        static void Main()
        {
            var manager = new HospitalManager();
            manager.RegisterPatient(1, "John Doe", 45, "Hypertension");
            manager.RegisterPatient(2, "Jane Smith", 32, "Diabetes");
            manager.ScheduleAppointment(1);
            manager.ScheduleAppointment(2);

            var nextPatient = manager.ProcessNextAppointment();
            Console.WriteLine(nextPatient?.Name ?? "None");

            var diabeticPatients = manager.FindPatientsByCondition("Diabetes");
            Console.WriteLine(diabeticPatients.Count);
        }
    }
}
