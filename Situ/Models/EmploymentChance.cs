using System;

namespace Situ.Models
{
    public class EmploymentChance
    {
        public Employee Person { get; set; }
        public DateTime FreeSince { get; set; } = DateTime.Today;
        public DateTime ExitDate { get; set; }
        public string State { get; set; }
        public string Info { get; set; }
    }
}
