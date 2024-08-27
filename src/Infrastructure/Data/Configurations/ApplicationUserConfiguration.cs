using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyWebApi.Domain.Entities;

namespace MyWebApi.Infrastructure.Data.Configurations
{
    public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            // builder.HasKey(user => user.Id);
            // builder.HasMany(e => e.Bookings)
            //         .WithOne()
            //         .HasForeignKey(e => e.GuestID)
            //         .IsRequired();

        }
    }
}
