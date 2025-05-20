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
        public DbSet<Glossary> Glossaries { get; set; }
        public DbSet<Word> Words { get; set; }

        public MultiDictionaryContext() { }
        public MultiDictionaryContext(DbContextOptions<MultiDictionaryContext> options)
            : base(options) { }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Word>()
                .HasOne(w => w.Glossary)
                .WithMany(g => g.Words)
                .HasForeignKey(w => w.GlossaryId)
                .HasPrincipalKey(g => g.Id) //EF core should do that auto, but works only with manual assignment
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Word>()
                .Property(word => word.Id)
                .UseIdentityColumn(1, 1);

            modelBuilder.Entity<Glossary>()
                .Property(glossary => glossary.Id)
                .UseIdentityColumn(1, 1);
        }
    }
}
