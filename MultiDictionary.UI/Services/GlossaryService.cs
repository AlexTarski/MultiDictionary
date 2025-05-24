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

        public void AddEntity(object model)
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

        public Task<GlossaryViewModel> GetByIdAsync(int id)
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
