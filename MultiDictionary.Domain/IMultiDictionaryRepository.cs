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
        Task<IEnumerable<Glossary>> GetAllGlossariesAsync();
        Task<Glossary> GetGlossaryByIdAsync(int id);
        Task<IEnumerable<Word>> GetAllWordsAsync();
        Task<IEnumerable<Word>> GetWordsByGlossaryAsync(int glossaryId);
        Task<IEnumerable<Word>> GetAllWordsByThemeAsync(int glossaryId, string theme);
        Task<Word> GetWordByIdAsync(int id);
        void AddEntity(Object model);
        Task UpdateEntityAsync(Object model);
        Task DeleteEntityAsync(int id);
        bool SaveAll();
    }
}
