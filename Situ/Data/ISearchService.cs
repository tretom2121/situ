using System.Collections.Generic;
using System.Threading.Tasks;

namespace Situ.Data
{
    public interface ISearchService<T>
    {
        Task<List<T>> GetMatchingItems(string searchText);
        Task<T> GetItemById(int id);
    }
}
