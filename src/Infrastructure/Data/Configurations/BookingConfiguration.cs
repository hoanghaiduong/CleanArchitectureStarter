
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyWebApi.Domain.Entities;
using MyWebApi.Infrastructure.Identity;

namespace MyWebApi.Infrastructure.Data.Configurations
{
    public class BookingConfiguration : IEntityTypeConfiguration<Booking>
    {
        public void Configure(EntityTypeBuilder<Booking> entity)
        {
            entity.HasKey(e => e.BookingID);

            // Configure relationship with ApplicationUser
            entity.HasOne<ApplicationUser>() // Assuming ApplicationUser is added to the model
                  .WithMany(user => user.Bookings)
                  .HasForeignKey(e => e.GuestID)
                  .OnDelete(DeleteBehavior.Restrict); // Or another appropriate behavior

      
        }
    }
}