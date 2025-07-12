using MultiDictionary.App.Interfaces;
using MultiDictionary.Domain;
using MultiDictionary.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        public async Task<bool> AddEntityAsync(object model)
        {
            var isValid = await EntityIsValidAsync(model);

            if (!isValid)
            {
                throw new ValidationException("Model validation failed");
            }

            await _repo.AddEntityAsync(model);
            return await SaveAllAsync();
        }

        public async Task<bool> UpdateEntityAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteEntityAsync(int id)
        {
            var wordToDelete = await GetByIdAsync(id);

            if (wordToDelete == null)
            {
                throw new KeyNotFoundException($"Word with ID {id} not found");
            }

            _repo.DeleteEntity(wordToDelete);
            return await SaveAllAsync();
        }

        public async Task<IEnumerable<Word>> GetAllAsync()
        {
            return await _repo.GetAllWordsAsync();
        }

        public async Task<IEnumerable<Word>> GetWordsByGlossaryAsync(int glossaryId)
        {
            if (!await _repo.IsGlossaryExistingAsync(glossaryId))
            {
                throw new KeyNotFoundException($"Glossary with ID {glossaryId} is not found");
            }

            return await _repo.GetWordsByGlossaryAsync(glossaryId);
        }

        public async Task<IEnumerable<Word>> GetWordsByThemeAsync(int glossaryId, string theme)
        {
            if(!await _repo.IsGlossaryExistingAsync(glossaryId))
            {
                throw new KeyNotFoundException($"Glossary with ID {glossaryId} is not found");
            }
            else
            {
                if(string.IsNullOrWhiteSpace(theme))
                {
                    throw new ArgumentException("Theme is null, empty or consists only of white-space characters", nameof(theme));
                }

                return await _repo.GetWordsByThemeAsync(glossaryId, theme);
            }
        }

        public async Task<Word> GetByIdAsync(int id)
        {
            var result = await _repo.GetWordByIdAsync(id);
            if(result == null)
                throw new KeyNotFoundException($"Word with ID {id} was not found");
            return result;
        }

        public async Task<bool> EntityIsValidAsync(object model)
        {
            if(model is Word newWord)
            {
                if (!await _repo.IsGlossaryExistingAsync(newWord.GlossaryId))
                {
                    throw new KeyNotFoundException("Impossible to add a new word inside the glossary that doesn`t exist");
                }
                // Assign defaults if null
                newWord.Theme ??= "No theme";
                newWord.Definition ??= "No definition";
                newWord.AdditionalInfo ??= "No additional information about word";

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
