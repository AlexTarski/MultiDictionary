using MultiDictionary.Shared.ViewModels;
using MultiDictionary.UI.Interfaces;
using System.Net.Http.Json;

namespace MultiDictionary.UI.Services
{
    public class GlossaryService : IGlossaryService
    {
        private readonly HttpClient _httpClient;

        public GlossaryService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> AddEntityAsync(object model)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/glossaries", model);

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    var error = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Failed to add entity. Status: {response.StatusCode}, Error: {error}");
                }
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Network error: {ex.Message}");
            }

            return false;
        }

        public Task UpdateEntityAsync(object model)
        {
            throw new NotImplementedException();
        }

        public Task DeleteEntityAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<GlossaryViewModel>> GetAllAsync(bool includeWords)
        {
            try
            {
                string endpoint = includeWords ? "api/glossaries?includeWords=true" : "api/glossaries";
                return await _httpClient.GetFromJsonAsync<IEnumerable<GlossaryViewModel>>(endpoint);
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Network error: {ex.Message}");
                return new List<GlossaryViewModel>(); // Return empty list instead of crashing
            }
        }

        public async Task<GlossaryViewModel> GetByIdAsync(int id)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<GlossaryViewModel>($"api/glossaries/{id}");
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Network error: {ex.Message}");
                return new GlossaryViewModel(); // Return empty list instead of crashing
            }
        }

        public Task<bool> SaveAllAsync()
        {
            throw new NotImplementedException();
        }
    }
}
