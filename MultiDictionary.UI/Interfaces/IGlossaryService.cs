using MultiDictionary.Shared.ViewModels;

namespace MultiDictionary.UI.Interfaces
{
    public interface IGlossaryService : IEntityService<GlossaryViewModel>
    {
        public Task<IEnumerable<GlossaryViewModel>> GetAllAsync(bool includeWords);
    }
}
