using CA_Final_Regia.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA_Final_Regia.Infrastructure.DataBase.Configuration
{
    internal class LocationConfiguration : IEntityTypeConfiguration<Location>
    {
        public void Configure(EntityTypeBuilder<Location> builder)
        {
            builder.HasKey(l => l.AccountId);
            builder.Property(l => l.AccountId)
                .IsRequired();
            builder.Property(l => l.City)
                .HasMaxLength(100)
                .IsRequired();
            builder.Property(l => l.Street)
                .HasMaxLength(100)
                .IsRequired();
            builder.Property(l => l.HouseNr)
                .HasMaxLength(5)
                .IsRequired();
            builder.Property(l => l.ApartmentNr)
                .HasMaxLength(4)
                .IsRequired();
            builder.HasOne(a => a.Person)
                .WithOne(p => p.Location)
                .HasForeignKey<Person>(a => a.AccountId);
            builder.HasIndex(a => a.AccountId)
                .IsUnique();
        }
    }
}
