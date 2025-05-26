using MultiDictionary.Domain.Entities;
using MultiDictionary.Shared.ViewModels;
using MultiDictionary.UI.Interfaces;
using System.Net.Http.Json;

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

        public async Task<WordViewModel> GetByIdAsync(int id)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<WordViewModel>($"api/words/{id}");
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Network error: {ex.Message}");
                return new WordViewModel(); // Return empty instead of crashing
            }
        }

        public async Task<IEnumerable<WordViewModel>> GetWordsByGlossaryAsync(int glossaryId)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<IEnumerable<WordViewModel>>($"api/words/glossary/{glossaryId}");
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Network error: {ex.Message}");
                return new List<WordViewModel>(); // Return empty list instead of crashing
            }
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
