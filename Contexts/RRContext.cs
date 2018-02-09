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

        public virtual DbSet<Review> Reviews { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql(Environment.GetEnvironmentVariable("DATABASE_CS"));
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

                entity.Property(e => e.Location).HasColumnName("location");

                entity.HasMany(r => r.Reviews).WithOne(rev => rev.Restroom).HasForeignKey(e => e.RestroomId);

            });

            modelBuilder.Entity<Review>(entity => 
            {
                entity.ToTable("reviews", "dbo");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("nextval('dbo.reviews_id_seq'::regclass)");

                entity.Property(e => e.CreateDate)
                    .HasColumnName("createdate")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.Description).HasColumnName("description");
                
                entity.Property(e => e.Title).HasColumnName("title");

                entity.Property(e => e.Rating).HasColumnName("rating");

                entity.Property(e => e.Author).HasColumnName("author");

                entity.Property(e => e.RestroomId).HasColumnName("restroomId");

                // entity.HasOne(e => e.Restroom)
                //     .WithMany(r => r.Reviews)
                //     .HasForeignKey(e => e.Id)
                //     .HasConstraintName("restrooms.id");


            });

            modelBuilder.HasSequence("reviews_id_seq")
                .HasMin(1)
                .HasMax(2147483647);

            modelBuilder.HasSequence("restrooms_id_seq")
                .HasMin(1)
                .HasMax(2147483647);
        }
    }
}