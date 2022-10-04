using Situ.Models;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Situ.Data
{
    public class ReportService
    {
        public async Task SaveReportAsync(Report report)
        {
            var path = Path.Combine(CentralVariables.GetStorageFolder(), CentralVariables.GetDepartmentSubFolderName(report.Department));
            Directory.CreateDirectory(path);

            var fileName = CentralVariables.GetReportFileName(report.Date);
            var filePathAndName = Path.Combine(path, fileName);
            using (var stream = new FileStream(filePathAndName, FileMode.Create))
            {
                await System.Text.Json.JsonSerializer.SerializeAsync(stream, report);
            }
        }

        public Report GetLastReportByDepartment(Department department)
        {
            var path = Path.Combine(CentralVariables.GetStorageFolder(), CentralVariables.GetDepartmentSubFolderName(department));
            if (!Directory.Exists(path)) return null;

            var files = new DirectoryInfo(path).GetFiles();

            FileInfo lastFile = null;
            foreach (var file in files)
            {
                if (string.Compare(file.Name, lastFile?.Name, StringComparison.InvariantCulture) >= 0) lastFile = file;
            }

            var jsonstring = File.ReadAllText(lastFile.FullName);
            return System.Text.Json.JsonSerializer.Deserialize<Report>(jsonstring);
        }

        public Report GetReportByDepartmentAndDate(Department department, DateTime date)
        {
            var path = Path.Combine(CentralVariables.GetStorageFolder(), CentralVariables.GetDepartmentSubFolderName(department));
            if (!Directory.Exists(path)) return null;

            var files = new DirectoryInfo(path).GetFiles();

            FileInfo correspondingFile = new(CentralVariables.GetReportFileName(date));

            foreach (var file in files)
            {
                if (string.Compare(file.Name, correspondingFile?.Name, StringComparison.InvariantCulture) == 0)
                {
                    correspondingFile = file;
                    break;
                }
            }

            if (!File.Exists(correspondingFile.FullName)) return null;

            var jsonstring = File.ReadAllText(correspondingFile.FullName);
            return System.Text.Json.JsonSerializer.Deserialize<Report>(jsonstring);
        }
    }
}
