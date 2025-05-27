using MultiDictionary.Domain.Entities;
using MultiDictionary.Shared.ViewModels;
using MultiDictionary.UI.Interfaces;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;

namespace MultiDictionary.UI.Services
{
    public class WordService : IWordService
    {
        private readonly HttpClient _httpClient;

        public WordService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> AddEntityAsync(object model)
        {
            throw new NotImplementedException();
        }

        public Task DeleteEntityAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<WordViewModel>> GetAllAsync()
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<IEnumerable<WordViewModel>>($"api/words/");
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Network error: {ex.Message}");
                return new List<WordViewModel>(); // Return empty list instead of crashing
            }
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

        public async Task<IEnumerable<WordViewModel>> GetWordsByThemeAsync(int glossaryId, string theme)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<IEnumerable<WordViewModel>>($"api/words/glossary/{glossaryId}/theme?theme={theme}");
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Network error: {ex.Message}");
                return new List<WordViewModel>(); // Return empty list instead of crashing
            }
        }

        public Task UpdateEntityAsync(object model)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SaveAllAsync()
        {
            throw new NotImplementedException();
        }
    }
}
