using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyWebApi.Domain.Entities;

namespace MyWebApi.Infrastructure.Data.Configurations;
public class HotelConfiguration : IEntityTypeConfiguration<Hotel>
{
    public void Configure(EntityTypeBuilder<Hotel> builder)
    {
        builder.HasKey(e => e.HotelId).HasName("PK__Hotel__46023BBF7B2182CF");
        builder.ToTable("Hotel");
        builder.Property(e => e.HotelId)
                .ValueGeneratedNever()
                .HasColumnName("HotelID");
        builder.Property(e => e.Address)
            .HasMaxLength(255)
            .IsUnicode(false);
        builder.Property(e => e.Email)
            .HasMaxLength(255)
            .IsUnicode(false);
        builder.Property(e => e.Name)
            .HasMaxLength(255)
            .IsUnicode(false);
        builder.Property(e => e.Phone)
            .HasMaxLength(15)
            .IsUnicode(false);
    }
}
