﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyWebApi.Infrastructure.Data;
using System;
using System.Collections.Generic;

#nullable disable

namespace MyWebApi.Infrastructure.Data.Configurations
{
    public partial class RoomTypeConfiguration : IEntityTypeConfiguration<RoomType>
    {
        public void Configure(EntityTypeBuilder<RoomType> entity)
        {
            entity.HasKey(e => e.TypeID).HasName("PK__RoomType__516F0395E615E4F8");

            entity.ToTable("RoomType");

            entity.Property(e => e.TypeID).ValueGeneratedNever();
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PricePerNight).HasColumnType("decimal(10, 2)");

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<RoomType> entity);
    }
}
