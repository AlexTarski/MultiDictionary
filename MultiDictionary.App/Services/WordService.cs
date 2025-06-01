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
        public async Task AddEntityAsync(object model)
        {
            try
            {
                var isValid = await EntityIsValid(model);

                if (!isValid)
                {
                    throw new ArgumentException("Model validation failed");
                }

                await _repo.AddEntityAsync(model);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to add word: {ex.Message}");
            }            
        }

        public void UpdateEntity(object model)
        {
            _repo.UpdateEntity(model);
        }

        public void DeleteEntity(object model)
        {
            _repo.DeleteEntity(model);
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

        public async Task<bool> EntityIsValid(Object model)
        {
            if(model is Word newWord)
            {
                if (!await _repo.IsGlossaryExistingAsync(newWord.GlossaryId))
                {
                    throw new ArgumentException("Impossible to add a new word inside the glossary that doesn`t exist");
                }

                return true;
            }
            else
            {
                throw new ArgumentException("Model is not a Word", nameof(model));
            }
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _repo.SaveAllAsync();
        }
    }
}
