using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiDictionary.App.Interfaces
{
    public interface IEntityService<T> 
        where T : class
    {
        Task<T> GetByIdAsync(int id);
        Task<bool> AddEntityAsync(Object model);
        Task<bool> UpdateEntityAsync(int id);
        Task<bool> DeleteEntityAsync(int id);
        Task<bool> EntityIsValidAsync(Object model);
        Task<bool> SaveAllAsync();
    }
}
