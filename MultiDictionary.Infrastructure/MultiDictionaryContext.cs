using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MultiDictionary.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiDictionary.Infrastructure
{
    public class MultiDictionaryContext : DbContext
    {
        private readonly IConfiguration _config;

        public DbSet<Glossary> Glossaries { get; set; }
        public DbSet<Word> Words { get; set; }

        public MultiDictionaryContext(IConfiguration config)
        {
            _config = config;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_config["ConnectionStrings:MultiDictionary"]);
        }
    }
}
