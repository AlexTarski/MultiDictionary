using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiDictionary.Domain.Entities
{
    public class Word
    {
        public int Id { get; set; }
        public string WordName { get; set; }
        public string Theme { get; set; }
        public string Definition { get; set; }
        public string AdditionalInfo { get; set; }
        public Glossary Glossary { get; set; }
        public int GlossayId { get; set; }
    }
}
