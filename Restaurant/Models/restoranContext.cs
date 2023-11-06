﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Restaurant.Models;

public partial class restoranContext : DbContext
{
    public restoranContext()
    {
    }

    public restoranContext(DbContextOptions<restoranContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Article> Articles { get; set; }

    public virtual DbSet<ArticleType> ArticleTypes { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderItem> OrderItems { get; set; }

    public virtual DbSet<Reservation> Reservations { get; set; }

    public virtual DbSet<Restaurant> Restaurants { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        {
            if (!optionsBuilder.IsConfigured)
            {
                var builder = new ConfigurationBuilder()
                    .AddUserSecrets<restoranContext>();

                var configuration = builder.Build();

                optionsBuilder.UseMySql(configuration["ConnectionStrings:MyDbConnection"], Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.34-mysql"));
            }
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb3_general_ci")
            .HasCharSet("utf8mb3");

        modelBuilder.Entity<Article>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("article");

            entity.HasIndex(e => e.IdType, "fk_ARTIKAL_VRSTA_ARTIKLA1_idx");

            entity.Property(e => e.Description)
                .IsRequired()
                .HasMaxLength(200);
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(60);
            entity.Property(e => e.Price).HasPrecision(5, 2);

            entity.HasOne(d => d.IdTypeNavigation).WithMany(p => p.Articles)
                .HasForeignKey(d => d.IdType)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_ARTIKAL_VRSTA_ARTIKLA1");
        });

        modelBuilder.Entity<ArticleType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("article_type");

            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(45);
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.Jmb).HasName("PRIMARY");

            entity.ToTable("employee");

            entity.Property(e => e.Jmb)
                .HasMaxLength(13)
                .IsFixedLength()
                .HasColumnName("JMB");
            entity.Property(e => e.Address)
                .IsRequired()
                .HasMaxLength(45);
            entity.Property(e => e.Language).HasMaxLength(45);
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(20);
            entity.Property(e => e.Password)
                .IsRequired()
                .HasMaxLength(100);
            entity.Property(e => e.Prezime)
                .IsRequired()
                .HasMaxLength(20);
            entity.Property(e => e.Telephone)
                .IsRequired()
                .HasMaxLength(45);
            entity.Property(e => e.Theme).HasMaxLength(45);
            entity.Property(e => e.Username)
                .IsRequired()
                .HasMaxLength(45);
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderNumber).HasName("PRIMARY");

            entity.ToTable("order");

            entity.HasIndex(e => e.Jmbemployee, "fk_NARUDŽBA_ZAPOSLENI1_idx");

            entity.Property(e => e.IssueDateAndTime).HasColumnType("datetime");
            entity.Property(e => e.Jmbemployee)
                .IsRequired()
                .HasMaxLength(13)
                .IsFixedLength()
                .HasColumnName("JMBEmployee");

            entity.HasOne(d => d.JmbemployeeNavigation).WithMany(p => p.Orders)
                .HasForeignKey(d => d.Jmbemployee)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_NARUDŽBA_ZAPOSLENI1");
        });

        modelBuilder.Entity<OrderItem>(entity =>
        {
            entity.HasKey(e => e.IdItem).HasName("PRIMARY");

            entity.ToTable("order_item");

            entity.HasIndex(e => e.IdArticle, "fk_STAVKA_NARUDŽBE_ARTIKAL1_idx");

            entity.HasIndex(e => e.OrderNumber, "fk_STAVKA_NARUDŽBE_NARUDŽBA1_idx");

            entity.Property(e => e.Price).HasPrecision(5, 2);

            entity.HasOne(d => d.IdArticleNavigation).WithMany(p => p.OrderItems)
                .HasForeignKey(d => d.IdArticle)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_STAVKA_NARUDŽBE_ARTIKAL1");

            entity.HasOne(d => d.OrderNumberNavigation).WithMany(p => p.OrderItems)
                .HasForeignKey(d => d.OrderNumber)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_STAVKA_NARUDŽBE_NARUDŽBA1");
        });

        modelBuilder.Entity<Reservation>(entity =>
        {
            entity.HasKey(e => e.IdReservation).HasName("PRIMARY");

            entity.ToTable("reservation");

            entity.Property(e => e.DateAndTime).HasColumnType("datetime");
            entity.Property(e => e.PersonName)
                .IsRequired()
                .HasMaxLength(45);
        });

        modelBuilder.Entity<Restaurant>(entity =>
        {
            entity.HasKey(e => e.Name).HasName("PRIMARY");

            entity.ToTable("restaurant");

            entity.Property(e => e.Name).HasMaxLength(45);
            entity.Property(e => e.Address)
                .IsRequired()
                .HasMaxLength(100);
            entity.Property(e => e.Telephone)
                .IsRequired()
                .HasMaxLength(45);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}