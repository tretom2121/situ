using Situ.Models;
using System;

namespace Situ.Data
{
    public static class CentralVariables
    {
        public const string ADMIN_ROLE = "admin";
        public const string ADMINISTRATION_ROLE = "administration";
        public const string DISPATCH_ROLE = "disponent";
        public const string MANAGEMENT_ROLE = "management";
        public const string RECRUITING_ROLE = "recruiting";
        public static DateTime TRIAL_PERIOD_END = new(2022, 12, 31);

        public static string WorkingFolderRoot { get; set; }

        public static string GetReportFileName(DateTime reportDate)
        {
            return $"{reportDate:yyyy_MM_dd}.json";
        }

        public static string GetDepartmentSubFolderName(Department department)
        {
            return $"{department.Id}";
        }

        public static string GetStorageFolder()
        {
            return WorkingFolderRoot;
        }
    }
}
