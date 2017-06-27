using Bookstore.Core.Entities;
using Bookstore.Data.Configurations;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookstore.Data
{
    public class BookstoreDbContext : DbContext
    {
        public DbSet<BookEntity> Books { get; set; }

        public BookstoreDbContext()
            : base("DefaultConnection") { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            Database.SetInitializer<BookstoreDbContext>(new DropCreateDatabaseIfModelChanges<BookstoreDbContext>());

            modelBuilder.Configurations.Add(new BookConfiguration());
        }
    }
}
