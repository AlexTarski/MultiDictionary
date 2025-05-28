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
        public async Task AddEntityAsync(object model)
        {
            await _context.AddAsync(model);
        }

        public void UpdateEntity(object model)
        {
            _context.Update(model);
        }

        public void DeleteEntity(object model)
        {
            _context.Remove(model);
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

        public async Task<IEnumerable<Word>> GetWordsByGlossaryAsync(int glossaryId)
        {
            return await _context.Words.
                Where(word => word.GlossaryId == glossaryId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Word>> GetWordsByThemeAsync(int glossaryId, string theme)
        {
            return await _context.Words.
                Where(word => word.GlossaryId == glossaryId && word.Theme == theme)
                .ToListAsync();
        }

        public async Task<Glossary> GetGlossaryByIdAsync(int id)
        {
            return await _context.Glossaries
                .Include(glossary => glossary.Words)
                .FirstOrDefaultAsync(glossary => glossary.Id == id);
        }

        public async Task<Word> GetWordByIdAsync(int id)
        {
            return await _context.Words.FirstOrDefaultAsync(word => word.Id == id);
        }

        public async Task<bool> IsGlossaryExistingAsync(string name)
        {
            return await _context.Glossaries.AnyAsync(glossary => glossary.Name == name);
        }
        
        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
