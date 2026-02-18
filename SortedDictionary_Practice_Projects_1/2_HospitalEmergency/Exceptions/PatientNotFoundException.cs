namespace HospitalEmergency.Exceptions;

public class PatientNotFoundException : EmergencyException
{
    public string PatientId { get; }
    public PatientNotFoundException(string patientId) : base($"Patient not found: {patientId}.") => PatientId = patientId;
}
