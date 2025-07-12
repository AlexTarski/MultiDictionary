using MultiDictionary.App.Interfaces;
using MultiDictionary.Domain;
using MultiDictionary.Domain.Entities;
using System;
using System.Collections.Generic;
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
            throw new NotImplementedException();
            //if(model is Glossary newGlossary)
            //{
            //    if (await IsGlossaryExistingAsync(newGlossary.Name))
            //    {
            //        newGlossary.Name = await UpdateNameAsync(newGlossary.Name);
            //    }

            //    await _repo.AddEntityAsync(newGlossary);
            //}
            //else
            //{
            //    throw new ArgumentException("Model is not a Glossary", nameof(model));
            //}
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
            return await _repo.GetGlossaryByIdAsync(id);
        }

        public async Task<bool> IsGlossaryExistingAsync(string name)
        {
            return await _repo.IsGlossaryNameExistingAsync(name);
        }

        public Task<bool> EntityIsValidAsync(Object model)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _repo.SaveAllAsync();
        }

        private async Task<string> UpdateNameAsync(string name)
        {
            string newName = $"{name}_{new Random().Next(0, 99)}";
            while (await IsGlossaryExistingAsync(newName))
            {
                newName = $"{newName}{new Random().Next(0, 99)}";
            }

            return newName;
        }
    }
}