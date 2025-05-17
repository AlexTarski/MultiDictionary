using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiDictionary.Domain.Entities
{
    public class Glossary
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public HashSet<Word> Words { get; set; }
    }
}
