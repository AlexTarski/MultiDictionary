using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MultiDictionary.Domain;
using MultiDictionary.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiDictionary.Infrastructure
{
    public class MultiDictionaryRepository : IMultiDictionaryRepository
    {
        private readonly MultiDictionaryContext _context;
        private readonly ILogger<MultiDictionaryRepository> _logger;

        public MultiDictionaryRepository(MultiDictionaryContext context,
            ILogger<MultiDictionaryRepository> logger)
        {
            _context = context;
            _logger = logger;
        }
        public void AddEntity(object model)
        {
            _context.Add(model);
        }

        public Task DeleteEntityAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Glossary>> GetAllGlossariesAsync(bool includeWords)
        {
            if(includeWords)
            {
                return await _context.Glossaries
                    .Include(glossary => glossary.Words)
                    .ToListAsync();
            }
            else
            {
                return await _context.Glossaries
                    .ToListAsync();
            }
        }

        public async Task<IEnumerable<Word>> GetAllWordsAsync()
        {
            return await _context.Words .ToListAsync();
        }

        public Task<IEnumerable<Word>> GetWordsByGlossaryAsync(int glossaryId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Word>> GetWordsByThemeAsync(int glossaryId, string theme)
        {
            throw new NotImplementedException();
        }

        public Task<Glossary> GetGlossaryByIdAsync(int id, bool includeWords)
        {
            throw new NotImplementedException();
        }

        public Task<Word> GetWordByIdAsync(int id)
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
