using MultiDictionary.App.Interfaces;
using MultiDictionary.Domain;
using MultiDictionary.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiDictionary.App.Services
{
    public class GlossaryService : IGlossaryService
    {
        private readonly IMultiDictionaryRepository _repo;

        public GlossaryService(IMultiDictionaryRepository repo)
        {
            _repo = repo;
        }
        public async Task AddEntityAsync(object model)
        {
            await _repo.AddEntityAsync(model);
        }

        public Task UpdateEntityAsync(object model)
        {
            throw new NotImplementedException();
        }

        public Task DeleteEntityAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Glossary>> GetAllAsync(bool includeWords)
        {
            return await _repo.GetAllGlossariesAsync(includeWords);
        }

        public async Task<Glossary> GetByIdAsync(int id)
        {
            return await _repo.GetGlossaryByIdAsync(id);
        }

        public async Task<bool> IsGlossaryExistingAsync(string name)
        {
            return await _repo.IsGlossaryExistingAsync(name);
        }
        
        public async Task<bool> SaveAllAsync()
        {
            return await _repo.SaveAllAsync();
        }
    }
}
