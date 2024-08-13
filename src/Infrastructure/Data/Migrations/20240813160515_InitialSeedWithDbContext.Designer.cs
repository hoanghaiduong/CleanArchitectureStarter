﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MyWebApi.Infrastructure.Data;

#nullable disable

namespace MyWebApi.Infrastructure.Data.Migrations
{
    [DbContext(typeof(THUCHANHNET8_DAFContext))]
    [Migration("20240813160515_InitialSeedWithDbContext")]
    partial class InitialSeedWithDbContext
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("MyWebApi.Domain.Entities.Booking", b =>
                {
                    b.Property<int>("BookingID")
                        .HasColumnType("int");

                    b.Property<DateOnly?>("CheckinDate")
                        .HasColumnType("date");

                    b.Property<DateOnly?>("CheckoutDate")
                        .HasColumnType("date");

                    b.Property<int?>("GuestID")
                        .HasColumnType("int");

                    b.Property<int?>("RoomNumber")
                        .HasColumnType("int");

                    b.Property<decimal?>("TotalPrice")
                        .HasColumnType("decimal(10, 2)");

                    b.HasKey("BookingID")
                        .HasName("PK__Booking__73951ACD867D72D2");

                    b.ToTable("Booking", (string)null);
                });

            modelBuilder.Entity("MyWebApi.Domain.Entities.Event", b =>
                {
                    b.Property<int>("EventID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EventID"));

                    b.Property<DateTime?>("EventDateTime")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<string>("EventDescription")
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("EventType")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.HasKey("EventID")
                        .HasName("PK__Event__7944C870B86AC4A2");

                    b.ToTable("Event", (string)null);
                });

            modelBuilder.Entity("MyWebApi.Domain.Entities.Guest", b =>
                {
                    b.Property<int>("GuestID")
                        .HasColumnType("int");

                    b.Property<string>("Address")
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.Property<DateOnly?>("DateOfBirth")
                        .HasColumnType("date");

                    b.Property<string>("Email")
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("FirstName")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("LastName")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Phone")
                        .HasMaxLength(15)
                        .IsUnicode(false)
                        .HasColumnType("varchar(15)");

                    b.HasKey("GuestID")
                        .HasName("PK__Guest__0C423C32C3A031EA");

                    b.ToTable("Guest", (string)null);
                });

            modelBuilder.Entity("MyWebApi.Domain.Entities.Hotel", b =>
                {
                    b.Property<int>("HotelID")
                        .HasColumnType("int");

                    b.Property<string>("Address")
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.Property<TimeOnly?>("CheckinTime")
                        .HasColumnType("time");

                    b.Property<TimeOnly?>("CheckoutTime")
                        .HasColumnType("time");

                    b.Property<string>("Email")
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Name")
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Phone")
                        .HasMaxLength(15)
                        .IsUnicode(false)
                        .HasColumnType("varchar(15)");

                    b.Property<int?>("Stars")
                        .HasColumnType("int");

                    b.HasKey("HotelID")
                        .HasName("PK__Hotel__46023BBF5CAD8735");

                    b.ToTable("Hotel", (string)null);
                });

            modelBuilder.Entity("MyWebApi.Domain.Entities.Payment", b =>
                {
                    b.Property<int>("PaymentID")
                        .HasColumnType("int");

                    b.Property<decimal?>("Amount")
                        .HasColumnType("decimal(10, 2)");

                    b.Property<int?>("BookingID")
                        .HasColumnType("int");

                    b.Property<DateOnly?>("PaymentDate")
                        .HasColumnType("date");

                    b.Property<string>("PaymentMethod")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.HasKey("PaymentID")
                        .HasName("PK__Payment__9B556A58F2E06A4A");

                    b.ToTable("Payment", (string)null);
                });

            modelBuilder.Entity("MyWebApi.Domain.Entities.Room", b =>
                {
                    b.Property<int>("RoomNumber")
                        .HasColumnType("int");

                    b.Property<int?>("HotelID")
                        .HasColumnType("int");

                    b.Property<string>("Status")
                        .HasMaxLength(20)
                        .IsUnicode(false)
                        .HasColumnType("varchar(20)");

                    b.Property<int?>("TypeID")
                        .HasColumnType("int");

                    b.HasKey("RoomNumber")
                        .HasName("PK__Room__AE10E07B8CAA0ED0");

                    b.ToTable("Room", (string)null);
                });

            modelBuilder.Entity("MyWebApi.Domain.Entities.RoomType", b =>
                {
                    b.Property<int>("TypeID")
                        .HasColumnType("int");

                    b.Property<int?>("Capacity")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Name")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.Property<decimal?>("PricePerNight")
                        .HasColumnType("decimal(10, 2)");

                    b.HasKey("TypeID")
                        .HasName("PK__RoomType__516F0395E615E4F8");

                    b.ToTable("RoomType", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}
