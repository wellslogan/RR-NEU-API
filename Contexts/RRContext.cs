using System;
using Microsoft.EntityFrameworkCore;
using RR_NEU_API.Models;

namespace RR_NEU_API.Contexts {

  public partial class RRContext : DbContext
    {
        public RRContext(DbContextOptions<RRContext> options)
            :base(options) { }
        public RRContext(){ }
        public virtual DbSet<Restroom> Restrooms { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql(Environment.GetEnvironmentVariable("DATABASE_URL"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Restroom>(entity =>
            {
                entity.ToTable("restrooms", "dbo");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("nextval('dbo.restrooms_id_seq'::regclass)");

                entity.Property(e => e.CreateDate)
                    .HasColumnName("createdate")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.Latitude).HasColumnName("latitude");

                entity.Property(e => e.Longitude).HasColumnName("longitude");
            });

            modelBuilder.HasSequence("restrooms_id_seq")
                .HasMin(1)
                .HasMax(2147483647);
        }
    }
}