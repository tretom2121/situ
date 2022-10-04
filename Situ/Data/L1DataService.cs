using Situ.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Odbc;

namespace Situ.Data
{
    public class L1DataService : ISearchService<Employee>
    {
        private readonly List<Employee> _employees;
        public L1DataService()
        {
            //TODO remove this list when ODBC is done
            _employees = new List<Employee>
            {
                new Employee() { Id =1, FirstName = "Mertakan", LastName = "Adam" },
                new Employee() { Id =2, FirstName = "Jamaal", LastName = "Afaneh" },
                new Employee() { Id =3, FirstName = "David", LastName = "Agoston" },
                new Employee() { Id =4, FirstName = "Pinar", LastName = "Aksun" },
                new Employee() { Id =5, FirstName = "Nabil", LastName = "Al Aballah" },
                new Employee() { Id =6, FirstName = "Bashar", LastName = "Al Dares" },
                new Employee() { Id =7, FirstName = "Aleksandnar", LastName = "Angellov" },
                new Employee() { Id =8, FirstName = "Krasimir", LastName = "Angelov" },
                new Employee() { Id =9, FirstName = "Marin", LastName = "Angelov" },
                new Employee() { Id =10, FirstName = "Sabrina", LastName = "Apostel" },
                new Employee() { Id =11, FirstName = "Dibron", LastName = "Arifaj" },
                new Employee() { Id =12, FirstName = "Yasin", LastName = "Arslan" },
                new Employee() { Id =13, FirstName = "Mario", LastName = "Bagaric" },
                new Employee() { Id =14, FirstName = "Alfred", LastName = "Bahr" },
                new Employee() { Id =15, FirstName = "Kenaz", LastName = "Bakkour" },
                new Employee() { Id =16, FirstName = "Turgut", LastName = "Balaban" },
                new Employee() { Id =17, FirstName = "Miklos", LastName = "Balogh" },
                new Employee() { Id =18, FirstName = "Oemrer-Faruk", LastName = "Baralas" },
                new Employee() { Id =19, FirstName = "Zviadi", LastName = "Basilaia" },
                new Employee() { Id =20, FirstName = "Severirn", LastName = "Baruer" },
                new Employee() { Id =21, FirstName = "Ingerid", LastName = "Baumegärtner" },
                new Employee() { Id =22, FirstName = "Istvan", LastName = "Beilland" },
                new Employee() { Id =23, FirstName = "Karapet", LastName = "Beremperian" },
                new Employee() { Id =24, FirstName = "Edmond Raoul", LastName = "Bibbgaya" },
                new Employee() { Id =25, FirstName = "Tomislav", LastName = "Blareksic" },
                new Employee() { Id =26, FirstName = "Belal", LastName = "Banayan" },
                new Employee() { Id =27, FirstName = "Anzhel", LastName = "Borisov" },
                new Employee() { Id =28, FirstName = "Valentin", LastName = "Borisov" },
                new Employee() { Id =29, FirstName = "Hermann", LastName = "Brunmaier" },
                new Employee() { Id =30, FirstName = "Ivan", LastName = "Cacic" },
            };
        }

        public List<Employee> GetAllEmployeesAsync(Department department)
        {
            /*
            //TODO query string
            string queryString = "";

            var command = new OdbcCommand(queryString);

            using (OdbcConnection connection = new (department.L1ConnectionString))
            {
                command.Connection = connection;
                connection.Open();

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        //TODO read all employees
                    }
                }
            }
            */
            return _employees;
        }

        public async Task<Employee> GetItemById(int id)
        {
            return await Task.FromResult(_employees.FirstOrDefault(e => e.Id == id));
        }

        public Task<List<Employee>> GetMatchingItems(string searchText)
        {
            return Task.FromResult(_employees.Where(e => e.FullName.ToLower().Contains(searchText.ToLower())).ToList());
        }
    }
}
