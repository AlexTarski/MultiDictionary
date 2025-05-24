using MultiDictionary.Domain.Entities;
using MultiDictionary.Shared.ViewModels;

namespace MultiDictionary.UI.Interfaces
{
    public interface IWordService : IEntityService<WordViewModel>
    {
        Task<IEnumerable<WordViewModel>> GetAllAsync();
        Task<IEnumerable<WordViewModel>> GetWordsByGlossaryAsync(int glossaryId);
        Task<IEnumerable<WordViewModel>> GetWordsByThemeAsync(int glossaryId, string theme);
    }
}
