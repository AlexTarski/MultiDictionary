using MultiDictionary.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiDictionary.App.Interfaces
{
    public interface IWordService : IEntityService<Word>
    {
        Task<IEnumerable<Word>> GetAllWordsByThemeAsync(int glossaryId, string theme);
    }
}
