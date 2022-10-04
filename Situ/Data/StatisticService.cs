using Situ.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Situ.Data
{
    public class StatisticService
    {
        public DepartmentStats GetStatisticByDeptAndDate(Department department, DateTime fromDate, DateTime toDate)
        {
            var folder = CentralVariables.GetDepartmentSubFolderName(department);
            var fullPath = Path.Combine(CentralVariables.GetStorageFolder(), folder);
            
            var stats = new DepartmentStats(department);
            
            if (!Directory.Exists(fullPath)) return stats;
            
            var reportFiles = Directory.GetFiles(fullPath);
            reportFiles = reportFiles.Where(r => string.Compare(Path.GetFileName(r)[0..^5], fromDate.ToString("yyyy_MM_dd"), StringComparison.InvariantCulture) >= 0 && 
                                         string.Compare(Path.GetFileName(r)[0..^5], toDate.ToString("yyyy_MM_dd"), StringComparison.InvariantCulture) <= 0).ToArray();

            foreach (var reportFile in reportFiles)
            {
                var jsonstring = File.ReadAllText(reportFile);
                var report = System.Text.Json.JsonSerializer.Deserialize<Report>(jsonstring);
                stats.Statistics.Add(GetStatsFromReport(report));
            }

            return stats;
        }

        private Statistic GetStatsFromReport(Report report)
        {
            if (report == null) return null;
            return new Statistic()
            {
                Date = report.Date,
                ProzFehlzeitenquoteBezahlt = report.ProzFehlzeitenquoteBezahlt,
                ProzFehlzeitenquoteUnbezahlt = report.ProzFehlzeitenquoteUnbezahlt,
                ProzGesamtFehlzeitenquote = report.ProzGesamtFehlzeitenquote,
                ProzKrankheit = report.ProzKrankheit,
                ProzUnbezahlteFehlzeit = report.ProzUnbezahlteFehlzeit,
                ProzUrlaubUndGleitzeit = report.ProzUrlaubUndGleitzeit,
                ProzWartezeit = report.ProzWartezeit,
            };
        }
    }
}
