using System;
using System.Collections.Generic;

namespace Situ.Models
{
    public class DepartmentStats
    {
        private Department _department;

        public DepartmentStats(Department department)
        {
            _department = department ?? throw new ArgumentNullException(nameof(department));
        }
        public Department Department => _department;
        public List<Statistic> Statistics { get; set; } = new();
    }
}
