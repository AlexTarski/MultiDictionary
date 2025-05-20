using Microsoft.EntityFrameworkCore;
using MultiDictionary.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiDictionary.Infrastructure
{
    public class MultiDictionarySeeder
    {
        private readonly MultiDictionaryContext _context;

        public MultiDictionarySeeder(MultiDictionaryContext context)
        {
            _context = context;
        }

        public async Task SeedAsync()
        {
            if (!await _context.Glossaries.AnyAsync()) // Check if Glossary table is empty
            {

                var glossary = new Glossary
                {
                    Name = "Your first glossary"
                };

                _context.Glossaries.Add(glossary);
                await _context.SaveChangesAsync(); // Save the initial seed data

                var word = new Word
                {
                    WordName = "Word",
                    Theme = "Write your theme here",
                    Definition = "Write your word definition here",
                    AdditionalInfo = "Any additional info could be written here",
                    Glossary = glossary
                };

                _context.Words.Add(word);
                await _context.SaveChangesAsync(); // Save the initial seed data
            }
        }
    }
}
