using HospitalEmergency.Domain;
using HospitalEmergency.Exceptions;

namespace HospitalEmergency.Services;

/// <summary>SortedDictionary&lt;int, Queue&lt;Patient&gt;&gt; — key = severity (1 = Critical).</summary>
public class EmergencyQueueService
{
    private readonly SortedDictionary<int, Queue<Patient>> _queues = new();
    private readonly Dictionary<string, (int severity, Queue<Patient> queue)> _patientIndex = new();
    private const int MaxQueueSize = 100;

    private static void ValidateSeverity(int severity)
    {
        if (severity < 1 || severity > 5)
            throw new InvalidSeverityException(severity);
    }

    public void AddPatient(Patient patient)
    {
        if (patient == null) throw new ArgumentNullException(nameof(patient));
        ValidateSeverity(patient.Severity);
        if (_patientIndex.ContainsKey(patient.Id))
            return; // already in queue

        if (!_queues.ContainsKey(patient.Severity))
            _queues[patient.Severity] = new Queue<Patient>();
        var q = _queues[patient.Severity];
        if (q.Count >= MaxQueueSize)
            throw new QueueOverflowException(patient.Severity, MaxQueueSize);
        q.Enqueue(patient);
        _patientIndex[patient.Id] = (patient.Severity, q);
    }

    public Patient? GetNextPatient()
    {
        foreach (var kv in _queues)
        {
            if (kv.Value.Count > 0)
            {
                var p = kv.Value.Dequeue();
                _patientIndex.Remove(p.Id);
                return p;
            }
        }
        return null;
    }

    public void RemovePatient(string patientId)
    {
        if (!_patientIndex.TryGetValue(patientId, out var pair))
            throw new PatientNotFoundException(patientId);
        var q = pair.queue;
        var temp = new Queue<Patient>();
        Patient? toRemove = null;
        while (q.Count > 0)
        {
            var p = q.Dequeue();
            if (p.Id == patientId) toRemove = p;
            else temp.Enqueue(p);
        }
        while (temp.Count > 0) q.Enqueue(temp.Dequeue());
        _patientIndex.Remove(patientId);
    }

    public IReadOnlyDictionary<int, Queue<Patient>> GetAllQueues() => _queues;
}
