using MultiDictionary.Shared.ViewModels;
using MultiDictionary.UI.Interfaces;

namespace MultiDictionary.UI.Services
{
    public class WordService : IWordService
    {
        private readonly HttpClient _httpClient;

        public WordService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public void AddEntity(object model)
        {
            throw new NotImplementedException();
        }

        public Task DeleteEntityAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<WordViewModel>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<WordViewModel> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<WordViewModel>> GetWordsByGlossaryAsync(int glossaryId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<WordViewModel>> GetWordsByThemeAsync(int glossaryId, string theme)
        {
            throw new NotImplementedException();
        }

        public Task UpdateEntityAsync(object model)
        {
            throw new NotImplementedException();
        }

        public bool SaveAll()
        {
            throw new NotImplementedException();
        }
    }
}
