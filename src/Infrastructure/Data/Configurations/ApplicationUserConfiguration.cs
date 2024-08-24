using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyWebApi.Infrastructure.Identity;

namespace MyWebApi.Infrastructure.Data.Configurations
{
    public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.HasKey(user => user.Id);
            builder.HasMany(e => e.Bookings)
                    .WithOne()
                    .HasForeignKey(e => e.GuestID)
                    .IsRequired();

        }
    }
}
