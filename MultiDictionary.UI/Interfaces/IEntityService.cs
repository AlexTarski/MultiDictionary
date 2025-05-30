namespace MultiDictionary.UI.Interfaces
{
    public interface IEntityService<T>
        where T : class
    {
        Task<T> GetByIdAsync(int id);
        Task AddEntityAsync(Object model);
        Task UpdateEntityAsync(Object model);
        Task DeleteEntityAsync(int id);
    }
}
