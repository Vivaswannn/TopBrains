using System;
using System.Collections.Generic;
using System.Linq;

namespace HospitalManagementSystem
{
    public enum AppointmentStatus
    {
        Scheduled,
        Completed,
        Cancelled
    }

    public abstract class Person
    {
        public int Id { get; }
        public string Name { get; }
        public int Age { get; }

        protected Person(int id, string name, int age)
        {
            Id = id;
            Name = name;
            Age = age;
        }

        public override string ToString()
        {
            return $"{Id} - {Name} ({Age})";
        }
    }

    public class Doctor : Person, IBillable
    {
        public string Specialization { get; }
        public decimal ConsultationFee { get; }

        public Doctor(int id, string name, int age, string specialization, decimal fee)
            : base(id, name, age)
        {
            Specialization = specialization;
            ConsultationFee = fee;
        }

        public decimal CalculateBill(decimal baseAmount)
        {
            return baseAmount + ConsultationFee;
        }

        public override string ToString()
        {
            return base.ToString() + $" - {Specialization} - Fee: {ConsultationFee:C}";
        }
    }

    public class Patient : Person
    {
        public string Disease { get; }

        public Patient(int id, string name, int age, string disease)
            : base(id, name, age)
        {
            Disease = disease;
        }

        public override string ToString()
        {
            return base.ToString() + $" - Disease: {Disease}";
        }
    }

    public interface IBillable
    {
        decimal CalculateBill(decimal baseAmount);
    }

    public class MedicalRecord
    {
        private int _recordId;
        private Patient _patient;
        private string _diagnosis;
        private string _treatment;
        private DateTime _lastUpdated;

        public int RecordId
        {
            get { return _recordId; }
            private set { _recordId = value; }
        }

        public Patient Patient
        {
            get { return _patient; }
            private set { _patient = value; }
        }

        public string Diagnosis
        {
            get { return _diagnosis; }
            private set { _diagnosis = value; }
        }

        public string Treatment
        {
            get { return _treatment; }
            private set { _treatment = value; }
        }

        public DateTime LastUpdated
        {
            get { return _lastUpdated; }
            private set { _lastUpdated = value; }
        }

        public MedicalRecord(int recordId, Patient patient, string diagnosis, string treatment)
        {
            RecordId = recordId;
            Patient = patient;
            Diagnosis = diagnosis;
            Treatment = treatment;
            LastUpdated = DateTime.Now;
        }

        public void Update(string newDiagnosis, string newTreatment)
        {
            Diagnosis = newDiagnosis;
            Treatment = newTreatment;
            LastUpdated = DateTime.Now;
        }

        public override string ToString()
        {
            return $"Record {RecordId} - {Patient.Name} - {Diagnosis} - {Treatment} - {LastUpdated:g}";
        }
    }

    public class Appointment : IBillable
    {
        public int AppointmentId { get; }
        public Doctor Doctor { get; }
        public Patient Patient { get; }
        public DateTime StartTime { get; }
        public DateTime EndTime { get; }
        public AppointmentStatus Status { get; private set; }
        public decimal BaseAmount { get; }

        public Appointment(int id, Doctor doctor, Patient patient, DateTime start, DateTime end, decimal baseAmount)
        {
            AppointmentId = id;
            Doctor = doctor;
            Patient = patient;
            StartTime = start;
            EndTime = end;
            BaseAmount = baseAmount;
            Status = AppointmentStatus.Scheduled;
        }

        public void Complete()
        {
            if (Status == AppointmentStatus.Scheduled)
            {
                Status = AppointmentStatus.Completed;
            }
        }

        public void Cancel()
        {
            if (Status == AppointmentStatus.Scheduled)
            {
                Status = AppointmentStatus.Cancelled;
            }
        }

        public decimal CalculateBill(decimal baseAmount)
        {
            return Doctor.CalculateBill(baseAmount);
        }

        public decimal TotalBill()
        {
            return CalculateBill(BaseAmount);
        }

        public override string ToString()
        {
            return $"Appt {AppointmentId} - Dr: {Doctor.Name} - Pt: {Patient.Name} - {StartTime:g} - {Status} - Bill: {TotalBill():C}";
        }
    }

    public class DoctorNotAvailableException : Exception
    {
        public DoctorNotAvailableException(string message) : base(message) { }
    }

    public class InvalidAppointmentException : Exception
    {
        public InvalidAppointmentException(string message) : base(message) { }
    }

    public class PatientNotFoundException : Exception
    {
        public PatientNotFoundException(string message) : base(message) { }
    }

    public class DuplicateMedicalRecordException : Exception
    {
        public DuplicateMedicalRecordException(string message) : base(message) { }
    }

    public static class Hospital
    {
        public static List<Doctor> Doctors { get; } = new List<Doctor>();
        public static List<Patient> Patients { get; } = new List<Patient>();
        public static List<Appointment> Appointments { get; } = new List<Appointment>();
        public static Dictionary<int, MedicalRecord> MedicalRecords { get; } = new Dictionary<int, MedicalRecord>();

        private static int _nextAppointmentId = 1;
        private static int _nextRecordId = 1;

        public static void Seed()
        {
            if (Doctors.Any() || Patients.Any())
                return;

            Doctors.Add(new Doctor(1, "Dr. Rahul", 40, "Cardiology", 1500m));
            Doctors.Add(new Doctor(2, "Dr. Riya", 35, "Dermatology", 1000m));
            Doctors.Add(new Doctor(3, "Dr. John", 45, "Ortho", 1200m));

            Patients.Add(new Patient(1, "Ramesh", 30, "Heart Disease"));
            Patients.Add(new Patient(2, "Sneha", 25, "Skin Allergy"));
            Patients.Add(new Patient(3, "Vikas", 50, "Fracture"));
            Patients.Add(new Patient(4, "Rohan", 60, "Heart Disease"));

            var a1 = ScheduleAppointment(1, 1, DateTime.Now.AddDays(-5).AddHours(10), DateTime.Now.AddDays(-5).AddHours(11), 2000m);
            a1.Complete();
            var a2 = ScheduleAppointment(2, 2, DateTime.Now.AddDays(-20).AddHours(11), DateTime.Now.AddDays(-20).AddHours(12), 1500m);
            a2.Complete();
            var a3 = ScheduleAppointment(3, 3, DateTime.Now.AddDays(-2).AddHours(9), DateTime.Now.AddDays(-2).AddHours(10), 1800m);
            a3.Complete();
            var a4 = ScheduleAppointment(1, 4, DateTime.Now.AddDays(-1).AddHours(12), DateTime.Now.AddDays(-1).AddHours(13), 2200m);
            a4.Complete();

            CreateMedicalRecord(1, "Heart Disease", "Medication and checkups");
            CreateMedicalRecord(2, "Skin Allergy", "Ointment and tablets");
            CreateMedicalRecord(3, "Fracture", "Surgery and physio");
        }

        public static Doctor GetDoctor(int id)
        {
            var d = Doctors.FirstOrDefault(x => x.Id == id);
            if (d == null) throw new ArgumentException("Doctor not found.");
            return d;
        }

        public static Patient GetPatient(int id)
        {
            var p = Patients.FirstOrDefault(x => x.Id == id);
            if (p == null) throw new PatientNotFoundException("Patient not found.");
            return p;
        }

        public static Appointment ScheduleAppointment(int doctorId, int patientId, DateTime start, DateTime end, decimal baseAmount)
        {
            if (end <= start)
                throw new InvalidAppointmentException("End time must be after start time.");

            var doctor = GetDoctor(doctorId);
            var patient = GetPatient(patientId);

            bool overlap = Appointments.Any(a =>
                a.Doctor.Id == doctorId &&
                a.Status != AppointmentStatus.Cancelled &&
                start < a.EndTime &&
                end > a.StartTime);

            if (overlap)
                throw new DoctorNotAvailableException("Doctor is not available at this time.");

            var appt = new Appointment(_nextAppointmentId++, doctor, patient, start, end, baseAmount);
            Appointments.Add(appt);
            return appt;
        }

        public static MedicalRecord CreateMedicalRecord(int patientId, string diagnosis, string treatment)
        {
            var patient = GetPatient(patientId);
            int id = _nextRecordId++;
            if (MedicalRecords.ContainsKey(id))
                throw new DuplicateMedicalRecordException("Record id already exists.");

            var rec = new MedicalRecord(id, patient, diagnosis, treatment);
            MedicalRecords[id] = rec;
            return rec;
        }

        public static IEnumerable<Doctor> DoctorsWithMoreThan(int count)
        {
            return Appointments
                .GroupBy(a => a.Doctor)
                .Where(g => g.Count() > count)
                .Select(g => g.Key);
        }

        public static IEnumerable<Patient> PatientsTreatedInLastDays(int days)
        {
            var from = DateTime.Now.AddDays(-days);
            return Appointments
                .Where(a => a.Status == AppointmentStatus.Completed && a.StartTime >= from)
                .Select(a => a.Patient)
                .Distinct();
        }

        public static ILookup<Doctor, Appointment> AppointmentsByDoctor()
        {
            return Appointments.ToLookup(a => a.Doctor);
        }

        public static IEnumerable<(Doctor Doctor, decimal Total)> TopEarningDoctors(int top)
        {
            return Appointments
                .Where(a => a.Status == AppointmentStatus.Completed)
                .GroupBy(a => a.Doctor)
                .Select(g => new { Doctor = g.Key, Total = g.Sum(a => a.TotalBill()) })
                .OrderByDescending(x => x.Total)
                .Take(top)
                .Select(x => (x.Doctor, x.Total));
        }

        public static IEnumerable<Patient> PatientsByDisease(string disease)
        {
            return Patients.Where(p => p.Disease.Equals(disease, StringComparison.OrdinalIgnoreCase));
        }

        public static decimal TotalRevenue()
        {
            return Appointments
                .Where(a => a.Status == AppointmentStatus.Completed)
                .Sum(a => a.TotalBill());
        }

        public static IEnumerable<object> AppointmentReport()
        {
            return Appointments.Select(a => new
            {
                Id = a.AppointmentId,
                Doctor = a.Doctor.Name,
                Patient = a.Patient.Name,
                Time = a.StartTime,
                Status = a.Status,
                Bill = a.TotalBill()
            }).Cast<object>();
        }
    }

    public class Program
    {
        public static void Main()
        {
            Hospital.Seed();
            RunMenu();
        }

        private static void RunMenu()
        {
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("=== Hospital Management System ===");
                Console.WriteLine("1. List doctors");
                Console.WriteLine("2. List patients");
                Console.WriteLine("3. List appointments");
                Console.WriteLine("4. Schedule appointment");
                Console.WriteLine("5. Create medical record");
                Console.WriteLine("6. Doctors with >10 appointments");
                Console.WriteLine("7. Patients treated in last 30 days");
                Console.WriteLine("8. Group appointments by doctor");
                Console.WriteLine("9. Top 3 highest earning doctors");
                Console.WriteLine("10. Patients by disease");
                Console.WriteLine("11. Total revenue");
                Console.WriteLine("12. Export appointment report");
                Console.WriteLine("0. Exit");
                Console.Write("Choose: ");

                var choice = Console.ReadLine();
                Console.WriteLine();

                if (choice == "0") break;

                try
                {
                    switch (choice)
                    {
                        case "1": ListDoctors(); break;
                        case "2": ListPatients(); break;
                        case "3": ListAppointments(); break;
                        case "4": ScheduleAppointmentFromUser(); break;
                        case "5": CreateMedicalRecordFromUser(); break;
                        case "6": ShowBusyDoctors(); break;
                        case "7": ShowPatientsLast30Days(); break;
                        case "8": ShowAppointmentsByDoctor(); break;
                        case "9": ShowTopDoctors(); break;
                        case "10": ShowPatientsByDisease(); break;
                        case "11": ShowTotalRevenue(); break;
                        case "12": ShowAppointmentReport(); break;
                        default: Console.WriteLine("Invalid option."); break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
        }

        private static void ListDoctors()
        {
            foreach (var d in Hospital.Doctors)
                Console.WriteLine(d);
        }

        private static void ListPatients()
        {
            foreach (var p in Hospital.Patients)
                Console.WriteLine(p);
        }

        private static void ListAppointments()
        {
            foreach (var a in Hospital.Appointments)
                Console.WriteLine(a);
        }

        private static int ReadInt(string msg)
        {
            Console.Write(msg);
            return int.Parse(Console.ReadLine() ?? "0");
        }

        private static decimal ReadDecimal(string msg)
        {
            Console.Write(msg);
            return decimal.Parse(Console.ReadLine() ?? "0");
        }

        private static DateTime ReadDateTime(string msg)
        {
            Console.Write(msg);
            return DateTime.Parse(Console.ReadLine() ?? DateTime.Now.ToString("g"));
        }

        private static void ScheduleAppointmentFromUser()
        {
            ListDoctors();
            int did = ReadInt("Doctor id: ");
            ListPatients();
            int pid = ReadInt("Patient id: ");
            DateTime start = ReadDateTime("Start (e.g. 2026-02-17 10:00): ");
            int minutes = ReadInt("Duration minutes: ");
            decimal baseAmount = ReadDecimal("Base amount: ");

            DateTime end = start.AddMinutes(minutes);
            var appt = Hospital.ScheduleAppointment(did, pid, start, end, baseAmount);
            Console.WriteLine("Scheduled: " + appt);
        }

        private static void CreateMedicalRecordFromUser()
        {
            ListPatients();
            int pid = ReadInt("Patient id: ");
            Console.Write("Diagnosis: ");
            string diag = Console.ReadLine() ?? "";
            Console.Write("Treatment: ");
            string treat = Console.ReadLine() ?? "";

            var rec = Hospital.CreateMedicalRecord(pid, diag, treat);
            Console.WriteLine("Record created: " + rec);
        }

        private static void ShowBusyDoctors()
        {
            Console.WriteLine("Doctors with > 10 appointments:");
            foreach (var d in Hospital.DoctorsWithMoreThan(10))
                Console.WriteLine(d);
        }

        private static void ShowPatientsLast30Days()
        {
            Console.WriteLine("Patients treated in last 30 days:");
            foreach (var p in Hospital.PatientsTreatedInLastDays(30))
                Console.WriteLine(p);
        }

        private static void ShowAppointmentsByDoctor()
        {
            Console.WriteLine("Appointments by doctor:");
            var groups = Hospital.AppointmentsByDoctor();
            foreach (var g in groups)
            {
                Console.WriteLine(g.Key.Name + ":");
                foreach (var a in g)
                    Console.WriteLine("  " + a);
            }
        }

        private static void ShowTopDoctors()
        {
            Console.WriteLine("Top 3 earning doctors:");
            foreach (var (doc, total) in Hospital.TopEarningDoctors(3))
                Console.WriteLine($"{doc.Name} - {total:C}");
        }

        private static void ShowPatientsByDisease()
        {
            Console.Write("Disease: ");
            string d = Console.ReadLine() ?? "";
            Console.WriteLine("Patients with " + d + ":");
            foreach (var p in Hospital.PatientsByDisease(d))
                Console.WriteLine(p);
        }

        private static void ShowTotalRevenue()
        {
            Console.WriteLine("Total revenue: " + Hospital.TotalRevenue().ToString("C"));
        }

        private static void ShowAppointmentReport()
        {
            Console.WriteLine("Appointment report:");
            foreach (var item in Hospital.AppointmentReport())
                Console.WriteLine(item);
        }
    }
}

