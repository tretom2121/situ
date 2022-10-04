using System;
using System.Collections.Generic;

namespace Situ.Models
{
    public class Department
    {
        public Department(int id)
        {
            Id = id;
        }

        public int Id { get; }
        public string Name { get; set; }
        public string L1ConnectionString { get; set; }
        public List<User> Management { get; set; } = new List<User>();
        public List<User> Administration { get; set; } = new List<User>();
        public List<User> Dispatch { get; set; } = new List<User>();
        public List<User> Recruiting { get; set; } = new List<User>();
    }
}
