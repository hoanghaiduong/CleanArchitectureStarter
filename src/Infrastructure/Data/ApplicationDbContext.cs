using System.Reflection;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyWebApi.Application.Common.Interfaces;
using MyWebApi.Domain.Entities;

namespace MyWebApi.Infrastructure.Data;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
    //public virtual DbSet<Booking> Bookings { get; set; }

    //public virtual DbSet<Guest> Guests { get; set; }

    public virtual DbSet<Hotel> Hotels { get; set; }

    //public virtual DbSet<Payment> Payments { get; set; }

    //public virtual DbSet<Room> Rooms { get; set; }

    //public virtual DbSet<RoomType> RoomTypes { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
