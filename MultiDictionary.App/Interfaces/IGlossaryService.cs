using MultiDictionary.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiDictionary.App.Interfaces
{
    public interface IGlossaryService : IEntityService<Glossary>
    {
        public Task<IEnumerable<Glossary>> GetAllAsync(bool includeWords);
    }
}
