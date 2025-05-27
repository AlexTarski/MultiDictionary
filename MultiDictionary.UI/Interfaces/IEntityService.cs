namespace MultiDictionary.UI.Interfaces
{
    public interface IEntityService<T>
        where T : class
    {
        Task<T> GetByIdAsync(int id);
        Task<bool> AddEntityAsync(Object model);
        Task UpdateEntityAsync(Object model);
        Task DeleteEntityAsync(int id);
        Task<bool> SaveAllAsync();
    }
}
