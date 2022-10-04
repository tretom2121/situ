using Situ.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Situ.Data
{
    public class DepartmentService
    {
        private const string DEPT_FILE_NAME = "Departments.json";
        public async Task<List<Department>> GetAllDepartmentsAsync()
        {
            var departments = new List<Department>();
            string deptJsonFullPath = Path.Combine(CentralVariables.GetStorageFolder(), DEPT_FILE_NAME);

            if (File.Exists(deptJsonFullPath))
            {
                var deptsJson = await File.ReadAllTextAsync(deptJsonFullPath);
                departments = System.Text.Json.JsonSerializer.Deserialize<List<Department>>(deptsJson).OrderBy(u => u.Name).ToList();
            }

            return await Task.FromResult(departments);
        }

        public async Task<Department> GetDepartmentByIdAsync(int Id)
        {
            var departments = await GetAllDepartmentsAsync();
            var department = departments.FirstOrDefault(d => d.Id == Id);

            return await Task.FromResult(department);
        }

        public async Task SaveDepartment(Department department)
        {
            var allDepartments = await GetAllDepartmentsAsync();
            allDepartments.RemoveAll(d => d.Id == department.Id);
            allDepartments.Add(department);

            await SaveAllDepartments(allDepartments);
        }

        public async Task SaveAllDepartments(List<Department> allDepartments)
        {
            var filePathAndName = Path.Combine(CentralVariables.GetStorageFolder(), DEPT_FILE_NAME);
            using (var stream = new FileStream(filePathAndName, FileMode.Create))
            {
                await System.Text.Json.JsonSerializer.SerializeAsync(stream, allDepartments);
            }
        }

        public async Task DeleteDepartmentById(int Id)
        {
            var allDepartments = await GetAllDepartmentsAsync();
            allDepartments.RemoveAll(d => d.Id == Id);
            await SaveAllDepartments(allDepartments);
        }
    }
}
