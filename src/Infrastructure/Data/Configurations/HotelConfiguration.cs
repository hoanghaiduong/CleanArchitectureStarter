using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyWebApi.Domain.Entities;
using MyWebApi.Infrastructure.Identity;

namespace MyWebApi.Infrastructure.Data.Configurations
{
    public class HotelConfiguration : IEntityTypeConfiguration<Hotel>
    {
        public void Configure(EntityTypeBuilder<Hotel> builder)
        {

            builder.HasMany<ApplicationUser>()
                .WithOne(e => e.Hotel)
                .HasForeignKey(e => e.HotelID)
                .IsRequired(false);
        }
    }
}