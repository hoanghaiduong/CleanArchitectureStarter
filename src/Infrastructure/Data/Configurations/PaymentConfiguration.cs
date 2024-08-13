﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyWebApi.Infrastructure.Data;
using System;
using System.Collections.Generic;

#nullable disable

namespace MyWebApi.Infrastructure.Data.Configurations
{
    public partial class PaymentConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> entity)
        {
            entity.HasKey(e => e.PaymentID).HasName("PK__Payment__9B556A58F2E06A4A");

            entity.ToTable("Payment");

            entity.Property(e => e.PaymentID).ValueGeneratedNever();
            entity.Property(e => e.Amount).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.PaymentMethod)
                .HasMaxLength(50)
                .IsUnicode(false);

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<Payment> entity);
    }
}
