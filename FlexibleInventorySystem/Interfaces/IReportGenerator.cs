using System;

namespace FlexibleInventorySystem.Interfaces
{
    public interface IReportGenerator
    {
        string GenerateInventoryReport();
        string GenerateCategorySummary();
        string GenerateValueReport();
        string GenerateExpiryReport(int daysThreshold);
    }
}
