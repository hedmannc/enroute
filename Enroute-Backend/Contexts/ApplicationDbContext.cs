using System;
using System.Collections.Generic;
using EnrouteAppLibrary.Models;

using Microsoft.EntityFrameworkCore;

namespace Enroute_Backend.Contexts;

public partial class ApplicationDbContext : DbContext
{
    public ApplicationDbContext()
    {
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Building> Buildings { get; set; }

    public virtual DbSet<Location> Locations { get; set; }

    public virtual DbSet<Schedule> Schedules { get; set; }

    public virtual DbSet<ScheduleLocation> ScheduleLocations { get; set; }

    public virtual DbSet<UserLocationHistory> UserLocationHistories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Building>(entity =>
        {
            entity.Property(e => e.Latitude).HasColumnType("numeric(18, 0)");
            entity.Property(e => e.Longitude).HasColumnType("numeric(18, 0)");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Location>(entity =>
        {
            entity.Property(e => e.Description).IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Schedule>(entity =>
        {
            entity.ToTable("Schedule");

            entity.Property(e => e.UserId)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<ScheduleLocation>(entity =>
        {
            entity.HasNoKey();
        });

        modelBuilder.Entity<UserLocationHistory>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("UserLocationHistory");

            entity.Property(e => e.Latitude).HasColumnType("numeric(18, 0)");
            entity.Property(e => e.Longitude).HasColumnType("numeric(18, 0)");
            entity.Property(e => e.UserId)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
