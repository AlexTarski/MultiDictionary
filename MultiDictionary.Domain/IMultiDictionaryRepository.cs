using MultiDictionary.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiDictionary.Domain
{
    public interface IMultiDictionaryRepository 
    {
        Task<IEnumerable<Glossary>> GetAllGlossariesAsync(bool includeWords);
        Task<Glossary> GetGlossaryByIdAsync(int id);
        Task<IEnumerable<Word>> GetAllWordsAsync();
        Task<IEnumerable<Word>> GetWordsByGlossaryAsync(int glossaryId);
        Task<IEnumerable<Word>> GetWordsByThemeAsync(int glossaryId, string theme);
        Task<Word> GetWordByIdAsync(int id);
        Task AddEntityAsync(Object model);
        void UpdateEntity(Object model);
        void DeleteEntity(Object model);
        Task<bool> IsGlossaryExistingAsync(string name);
        Task<bool> SaveAllAsync();
    }
}
