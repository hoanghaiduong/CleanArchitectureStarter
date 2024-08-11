//namespace MyWebApi.Domain.Entities;

//public partial class ApplicationDbContext : DbContext
//{
//    public ApplicationDbContext()
//    {
//    }

//    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
//        : base(options)
//    {
//    }

//    public virtual DbSet<Booking> Bookings { get; set; }

//    public virtual DbSet<Guest> Guests { get; set; }

//    public virtual DbSet<Hotel> Hotels { get; set; }

//    public virtual DbSet<Payment> Payments { get; set; }

//    public virtual DbSet<Room> Rooms { get; set; }

//    public virtual DbSet<RoomType> RoomTypes { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        => optionsBuilder.UseSqlServer("Name=ConnectionStrings:DefaultConnectionMain");

//    protected override void OnModelCreating(ModelBuilder modelBuilder)
//    {
//        modelBuilder.Entity<Booking>(entity =>
//        {
//            entity.HasKey(e => e.BookingId).HasName("PK__Booking__73951ACD48940604");

//            entity.ToTable("Booking");

//            entity.Property(e => e.BookingId)
//                .ValueGeneratedNever()
//                .HasColumnName("BookingID");
//            entity.Property(e => e.GuestId).HasColumnName("GuestID");
//            entity.Property(e => e.TotalPrice).HasColumnType("decimal(10, 2)");

//            entity.HasOne(d => d.Guest).WithMany(p => p.Bookings)
//                .HasForeignKey(d => d.GuestId)
//                .HasConstraintName("FK__Booking__GuestID__2E1BDC42");

//            entity.HasOne(d => d.RoomNumberNavigation).WithMany(p => p.Bookings)
//                .HasForeignKey(d => d.RoomNumber)
//                .HasConstraintName("FK__Booking__RoomNum__2F10007B");
//        });

//        modelBuilder.Entity<Guest>(entity =>
//        {
//            entity.HasKey(e => e.GuestId).HasName("PK__Guest__0C423C3278BB8B07");

//            entity.ToTable("Guest");

//            entity.Property(e => e.GuestId)
//                .ValueGeneratedNever()
//                .HasColumnName("GuestID");
//            entity.Property(e => e.Address)
//                .HasMaxLength(255)
//                .IsUnicode(false);
//            entity.Property(e => e.Email)
//                .HasMaxLength(255)
//                .IsUnicode(false);
//            entity.Property(e => e.FirstName)
//                .HasMaxLength(50)
//                .IsUnicode(false);
//            entity.Property(e => e.LastName)
//                .HasMaxLength(50)
//                .IsUnicode(false);
//            entity.Property(e => e.Phone)
//                .HasMaxLength(15)
//                .IsUnicode(false);
//        });

//        modelBuilder.Entity<Hotel>(entity =>
//        {
//            entity.HasKey(e => e.HotelId).HasName("PK__Hotel__46023BBF7B2182CF");

//            entity.ToTable("Hotel");

//            entity.Property(e => e.HotelId)
//                .ValueGeneratedNever()
//                .HasColumnName("HotelID");
//            entity.Property(e => e.Address)
//                .HasMaxLength(255)
//                .IsUnicode(false);
//            entity.Property(e => e.Email)
//                .HasMaxLength(255)
//                .IsUnicode(false);
//            entity.Property(e => e.Name)
//                .HasMaxLength(255)
//                .IsUnicode(false);
//            entity.Property(e => e.Phone)
//                .HasMaxLength(15)
//                .IsUnicode(false);
//        });

//        modelBuilder.Entity<Payment>(entity =>
//        {
//            entity.HasKey(e => e.PaymentId).HasName("PK__Payment__9B556A585BA3B602");

//            entity.ToTable("Payment");

//            entity.Property(e => e.PaymentId)
//                .ValueGeneratedNever()
//                .HasColumnName("PaymentID");
//            entity.Property(e => e.Amount).HasColumnType("decimal(10, 2)");
//            entity.Property(e => e.BookingId).HasColumnName("BookingID");
//            entity.Property(e => e.PaymentMethod)
//                .HasMaxLength(50)
//                .IsUnicode(false);

//            entity.HasOne(d => d.Booking).WithMany(p => p.Payments)
//                .HasForeignKey(d => d.BookingId)
//                .HasConstraintName("FK__Payment__Booking__31EC6D26");
//        });

//        modelBuilder.Entity<Room>(entity =>
//        {
//            entity.HasKey(e => e.RoomNumber).HasName("PK__Room__AE10E07BBA83335D");

//            entity.ToTable("Room");

//            entity.Property(e => e.RoomNumber).ValueGeneratedNever();
//            entity.Property(e => e.HotelId).HasColumnName("HotelID");
//            entity.Property(e => e.Status)
//                .HasMaxLength(20)
//                .IsUnicode(false);
//            entity.Property(e => e.TypeId).HasColumnName("TypeID");

//            entity.HasOne(d => d.Hotel).WithMany(p => p.Rooms)
//                .HasForeignKey(d => d.HotelId)
//                .HasConstraintName("FK__Room__HotelID__286302EC");

//            entity.HasOne(d => d.Type).WithMany(p => p.Rooms)
//                .HasForeignKey(d => d.TypeId)
//                .HasConstraintName("FK__Room__TypeID__29572725");
//        });

//        modelBuilder.Entity<RoomType>(entity =>
//        {
//            entity.HasKey(e => e.TypeId).HasName("PK__RoomType__516F0395150B6500");

//            entity.ToTable("RoomType");

//            entity.Property(e => e.TypeId)
//                .ValueGeneratedNever()
//                .HasColumnName("TypeID");
//            entity.Property(e => e.Description)
//                .HasMaxLength(255)
//                .IsUnicode(false);
//            entity.Property(e => e.Name)
//                .HasMaxLength(50)
//                .IsUnicode(false);
//            entity.Property(e => e.PricePerNight).HasColumnType("decimal(10, 2)");
//        });

//        OnModelCreatingPartial(modelBuilder);
//    }

//    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
//}
