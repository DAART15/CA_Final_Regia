using CA_Final_Regia.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace CA_Final_Regia.Infrastructure.DataBase.Configuration
{
    public class AccountConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.HasKey(a => a.AccountId);
            builder.Property(a => a.UserName)
                .HasMaxLength(50)
                .IsRequired();
            builder.Property(a => a.Password)
                .IsRequired();
            builder.Property(a => a.Salt)
                .IsRequired();
            builder.Property(a => a.Role)
                .IsRequired();
            builder.HasIndex(a => a.UserName)
                .IsUnique();
            builder.HasIndex(a => a.AccountId)
                .IsUnique();
            builder.HasOne(a => a.Person)
                .WithOne(p => p.Account)
                .HasForeignKey<Person>(a => a.AccountId);

        }
    }
}
