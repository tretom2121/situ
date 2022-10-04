using Situ.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Situ.Data
{
    public class UserService
    {
        private const string USERS_FILE_NAME = "Users.json";

        public async Task<List<User>> GetAllUsersAsync()
        {
            var users = new List<User>();
            string usersJsonFullPath = Path.Combine(CentralVariables.GetStorageFolder(), USERS_FILE_NAME);

            if (File.Exists(usersJsonFullPath))
            {
                var usersJson = await File.ReadAllTextAsync(usersJsonFullPath);
                users = System.Text.Json.JsonSerializer.Deserialize<List<User>>(usersJson).OrderBy(u => u.LastName).ToList();
            }

            SetDefaultAdmins(users);

            return await Task.FromResult(users);
        }

        private void SetDefaultAdmins(List<User> users)
        {
            AddDefaultAdmin(users, "MusterMax", "Mustermann", "Max");
            AddDefaultAdmin(users, "DoeJohn", "Doe", "John");
            AddDefaultAdmin(users, "AdminTom", "Admin", "Tom");
        }

        private static void AddDefaultAdmin(List<User> users, string email, string lastName, string firstName)
        {
            var admin = users.FirstOrDefault(u => u.Email.ToLower() == email);
            if (admin == null)
            {
                users.Add(new User()
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Email = email,
                    Admin = true,
                });
            }
            else
            {
                admin.Admin = true;
            }
        }

        public async Task SaveAllUsers(List<User> users)
        {
            SetIdsForNewUsers(users);

            var path = Path.Combine(CentralVariables.GetStorageFolder());
            Directory.CreateDirectory(path);

            var fileName = $"Users.json";
            var filePathAndName = Path.Combine(path, fileName);
            using (var stream = new FileStream(filePathAndName, FileMode.Create))
            {
                await System.Text.Json.JsonSerializer.SerializeAsync(stream, users);
            }
        }

        private void SetIdsForNewUsers(List<User> users)
        {
            var freeId = int.MinValue;
            foreach (var user in users)
            {
                if (user.Id > freeId) freeId = user.Id;
            }

            freeId++;

            var newUsers = users.Where(u => u.Id == int.MinValue).ToList();

            foreach (var newUser in newUsers)
            {
                newUser.Id = freeId;
                freeId++;
            }
        }

        public async Task<List<string>> GetRoles(string userName)
        {
            var roles = new List<string>();

            var users = await GetAllUsersAsync();
            var user = users.FirstOrDefault(u => u.Email.ToLower() == userName.ToLower());
            if (user == null) return roles;

            var deptService = new DepartmentService();
            var depts = await deptService.GetAllDepartmentsAsync();

            foreach (var dept in depts)
            {
                if (dept.Administration.Any(a => a.Email == user.Email)) roles.Add($"{dept.Id}_{CentralVariables.ADMINISTRATION_ROLE}");
                if (dept.Dispatch.Any(a => a.Email == user.Email)) roles.Add($"{dept.Id}_{CentralVariables.DISPATCH_ROLE}");
                if (dept.Management.Any(a => a.Email == user.Email)) roles.Add($"{dept.Id}_{CentralVariables.MANAGEMENT_ROLE}");
                if (dept.Recruiting.Any(a => a.Email == user.Email)) roles.Add($"{dept.Id}_{CentralVariables.RECRUITING_ROLE}");
            }

            if (user.Admin) roles.Add(CentralVariables.ADMIN_ROLE);

            return roles;
        }
    }
}
