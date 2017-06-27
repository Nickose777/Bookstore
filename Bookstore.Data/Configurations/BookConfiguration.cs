using Bookstore.Core.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookstore.Data.Configurations
{
    class BookConfiguration : EntityTypeConfiguration<BookEntity>
    {
        public BookConfiguration()
        {
            this.HasKey(book => book.Id);

            this.Property(book => book.Title)
                .IsRequired()
                .HasMaxLength(30);
            this.Property(book => book.Author)
                .IsRequired()
                .HasMaxLength(40);
            this.Property(book => book.Price)
                .IsRequired()
                .HasPrecision(6, 2);
        }
    }
}
