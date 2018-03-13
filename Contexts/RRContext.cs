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

        public virtual DbSet<Author> Authors { get; set; }

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

                entity.Ignore(e => e.AverageRating);

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

                entity.Property(e => e.RestroomId).HasColumnName("restroom_id");

                entity.Property(e => e.AuthorId).HasColumnName("author_id");

                entity.Property(e => e.AuthorIsAnonymous).HasColumnName("author_is_anonymous");

                entity.HasOne(r => r.Author).WithMany(a => a.Reviews).HasForeignKey(e => e.AuthorId);
            });

            modelBuilder.Entity<Author>(entity =>
            {
                entity.ToTable("authors", "dbo");

                entity.Property(e => e.Id)
                      .HasColumnName("id")
                      .HasDefaultValueSql("nextval('dbo.authors_id_seq'::regclass)");

                entity.Property(e => e.GoogleId).HasColumnName("google_id");

                entity.Property(e => e.Name).HasColumnName("name");

                entity.HasMany(e => e.Reviews).WithOne(r => r.Author).HasForeignKey(e => e.AuthorId);

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