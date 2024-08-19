using CA_Final_Regia.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace CA_Final_Regia.Infrastructure.DataBase.Configuration
{
    internal class AccountConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            /*builder.HasKey(a =>a.AccountId);
            builder.Property(a => a.AccountId)
                .IsRequired()
                .ValueGeneratedNever();
            builder.HasIndex(a => a.AccountId)
                .IsUnique();*/

        }
    }
}
