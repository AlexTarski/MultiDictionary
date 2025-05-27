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
        Task AddEntityAsync(Object model);
        Task UpdateEntityAsync(Object model);
        Task DeleteEntityAsync(int id);
        Task<bool> SaveAllAsync();
    }
}
