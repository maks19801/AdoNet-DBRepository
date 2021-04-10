using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EFSamples
{
    class LibraryContext:DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Price> Prices { get; set; }
        public DbSet<Visitor> Visitors { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = @"Data Source = USER-PC; Initial Catalog = GeneratedLibrary; Integrated Security = True;";
            optionsBuilder.UseSqlServer(connectionString);
        }
    }
}
