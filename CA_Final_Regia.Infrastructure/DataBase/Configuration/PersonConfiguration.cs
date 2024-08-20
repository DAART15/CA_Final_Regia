using CA_Final_Regia.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CA_Final_Regia.Infrastructure.DataBase.Configuration
{
    public class PersonConfiguration : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.HasKey(p => p.AccountId);
            builder.Property(p => p.AccountId)
                .IsRequired();
            builder.Property(p => p.FirstName)
                .HasMaxLength(150)
                .IsRequired();
            builder.Property(p => p.LastName)
                .HasMaxLength(150)
                .IsRequired();
            builder.Property(p => p.PersonalId)
                .HasMaxLength(11)
                .IsRequired();
            builder.Property(p => p.PhoneNumber)
                .HasMaxLength(15)
                .IsRequired();
            builder.Property(p => p.Mail)
                .HasMaxLength(150)
                .IsRequired();
            builder.Property(p => p.FileData)
                .HasColumnType("varbinary(max)");
            builder.HasIndex(a => a.AccountId)
                .IsUnique();
        }
    }
}
