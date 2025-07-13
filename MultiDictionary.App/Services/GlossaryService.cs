using MultiDictionary.App.Interfaces;
using MultiDictionary.Domain;
using MultiDictionary.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
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
            var glossaryToDelete = await GetByIdAsync(id);
            if (glossaryToDelete == null)
            {
                throw new KeyNotFoundException($"Glossary with ID {id} not found");
            }

            _repo.DeleteEntity(glossaryToDelete);
            return await SaveAllAsync();
        }

        public async Task<IEnumerable<Glossary>> GetAllAsync(bool includeWords)
        {
            return await _repo.GetAllGlossariesAsync(includeWords);
        }

        public async Task<Glossary> GetByIdAsync(int id)
        {
            var result =  await _repo.GetGlossaryByIdAsync(id);
            if(result == null)
            {
                throw new KeyNotFoundException($"Glossary with ID {id} was not found");
            }

            return result;
        }

        public async Task<bool> IsGlossaryNameExistsAsync(string name)
        {
            return await _repo.IsGlossaryNameExistsAsync(name);
        }

        public async Task<bool> EntityIsValidAsync(Object model)
        {
            if(model is Glossary newGlossary)
            {
                if(await _repo.IsGlossaryExistsAsync(newGlossary.Id))
                {
                    throw new InvalidOperationException($"Glossary with ID {newGlossary.Id} already exists");
                }

                //change name if already exists
                if(await IsGlossaryNameExistsAsync(newGlossary.Name))
                {
                    newGlossary.Name = await UpdateNameAsync(newGlossary.Name);
                }

                return true;
            }
            else
            {
                throw new ArgumentException("Model is not a Glossary", nameof(model));
            }
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _repo.SaveAllAsync();
        }

        private async Task<string> UpdateNameAsync(string name)
        {
            string newName = $"{name}_{new Random().Next(0, 99)}";
            while (await IsGlossaryNameExistsAsync(newName))
            {
                newName = $"{newName}{new Random().Next(0, 99)}";
            }

            return newName;
        }
    }
}