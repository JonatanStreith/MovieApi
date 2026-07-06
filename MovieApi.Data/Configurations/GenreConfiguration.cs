using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieApi.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieApi.Data.Configurations
{
    internal class GenreConfiguration : IEntityTypeConfiguration<Genre>
    {
            {
        public void Configure(EntityTypeBuilder<Genre> builder)
        {
            builder.HasKey(g => g.GenreId);
            builder.Property(g => g.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnType("nvarchar")
                    ;


            builder.HasData(
                new Genre()
                {
                    GenreId = 1,
                    Name = "Action"
                },
                new Genre()
                {
                    GenreId = 2,
                    Name = "Comedy"
                },
                new Genre()
                {
                    GenreId = 3,
                    Name = "Thriller"
                },
                new Genre()
                {
                    GenreId = 4,
                    Name = "Documentary"
                },
                new Genre()
                {
                    GenreId = 5,
                    Name = "Erotica"
                }

                );
        }
    }


}
}
