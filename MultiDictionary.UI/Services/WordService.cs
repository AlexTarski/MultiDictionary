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

        public async Task AddEntityAsync(object model)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/words", model);
                if(!response.IsSuccessStatusCode)
                {
                    var error = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Failed to add entity. Status: {response.StatusCode}, Error: {error}");
                }
            }
            catch(HttpRequestException ex)
            {
                Console.WriteLine($"Network error: {ex.Message}");
            }
        }

        public async Task DeleteEntityAsync(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/words/{id}");

                if(!response.IsSuccessStatusCode)
                {
                    var error = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Failed to delete word. Status: {response.StatusCode}, Error: {error}");
                }
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Network error: {ex.Message}");
            }
        }

        public Task UpdateEntityAsync(object model)
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
    }
}
