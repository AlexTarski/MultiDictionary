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
    public class WordService : IWordService
    {
        private readonly IMultiDictionaryRepository _repo;

        public WordService(IMultiDictionaryRepository repo)
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

        public async Task<IEnumerable<Word>> GetAllAsync()
        {
            return await _repo.GetAllWordsAsync();
        }

        public async Task<IEnumerable<Word>> GetWordsByGlossaryAsync(int glossaryId)
        {
            return await _repo.GetWordsByGlossaryAsync(glossaryId);
        }

        public async Task<IEnumerable<Word>> GetWordsByThemeAsync(int glossaryId, string theme)
        {
            return await _repo.GetWordsByThemeAsync(glossaryId, theme);
        }

        public async Task<Word> GetByIdAsync(int id)
        {
            return await _repo.GetWordByIdAsync(id);
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
