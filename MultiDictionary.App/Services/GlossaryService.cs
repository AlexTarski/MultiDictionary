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
        public void AddEntity(object model)
        {
            _repo.AddEntity(model);
        }

        public Task DeleteEntityAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Glossary>> GetAllAsync(bool includeWords)
        {
            return await _repo.GetAllGlossariesAsync(includeWords);
        }

        public Task<Glossary> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public bool SaveAll()
        {
            throw new NotImplementedException();
        }

        public Task UpdateEntityAsync(object model)
        {
            throw new NotImplementedException();
        }
    }
}
