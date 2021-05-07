﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace _2._semesterprojekttest.Models
{
    public partial class GrydenDBContext : DbContext
    {
        public GrydenDBContext()
        {
        }

        public GrydenDBContext(DbContextOptions<GrydenDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Coupon> Coupons { get; set; }
        public virtual DbSet<CruizeUser> CruizeUsers { get; set; }
        public virtual DbSet<Driver> Drivers { get; set; }
        public virtual DbSet<Passenger> Passengers { get; set; }
        public virtual DbSet<Report> Reports { get; set; }
        public virtual DbSet<Request> Requests { get; set; }
        public virtual DbSet<Route> Routes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=alex-gryden-db.database.windows.net;Initial Catalog=\"Gryden DB\";Persist Security Info=True;User ID=adminlogin;Password=secret1!");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Coupon>(entity =>
            {
                entity.Property(e => e.Barcode).IsUnicode(false);

                entity.Property(e => e.Info).IsUnicode(false);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Coupons)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Coupon__UserID__76969D2E");
            });

            modelBuilder.Entity<CruizeUser>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK__CruizeUs__1788CCACBB8C9056");

                entity.Property(e => e.Address).IsUnicode(false);

                entity.Property(e => e.Email).IsUnicode(false);

                entity.Property(e => e.FirstName).IsUnicode(false);

                entity.Property(e => e.LastName).IsUnicode(false);

                entity.Property(e => e.Password).IsUnicode(false);
            });

            modelBuilder.Entity<Driver>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK__Driver__1788CCAC414E061E");

                entity.Property(e => e.UserId).ValueGeneratedNever();

                entity.Property(e => e.CarType).IsUnicode(false);

                entity.Property(e => e.Carcolor).IsUnicode(false);

                entity.HasOne(d => d.User)
                    .WithOne(p => p.Driver)
                    .HasForeignKey<Driver>(d => d.UserId)
                    .HasConstraintName("FK__Driver__UserID__797309D9");
            });

            modelBuilder.Entity<Passenger>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RouteId })
                    .HasName("PK__Passenge__DF81B50632808389");

                entity.HasOne(d => d.Route)
                    .WithMany(p => p.Passengers)
                    .HasForeignKey(d => d.RouteId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Passenger__Route__7F2BE32F");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Passengers)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Passenger__UserI__7E37BEF6");
            });

            modelBuilder.Entity<Report>(entity =>
            {
                entity.Property(e => e.Message).IsUnicode(false);

                entity.HasOne(d => d.ReportedNavigation)
                    .WithMany(p => p.ReportReportedNavigations)
                    .HasForeignKey(d => d.Reported)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Report__Reported__787EE5A0");

                entity.HasOne(d => d.ReporterNavigation)
                    .WithMany(p => p.ReportReporterNavigations)
                    .HasForeignKey(d => d.Reporter)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Report__Reporter__778AC167");
            });

            modelBuilder.Entity<Request>(entity =>
            {
                entity.Property(e => e.Message).IsUnicode(false);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Requests)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Request__UserID__7B5B524B");
            });

            modelBuilder.Entity<Route>(entity =>
            {
                entity.Property(e => e.Goal).IsUnicode(false);

                entity.Property(e => e.Start).IsUnicode(false);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Routes)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Route__UserID__7A672E12");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}