using Microsoft.EntityFrameworkCore;
using RR_NEU_API.Models;

namespace RR_NEU_API.Repository {

  public class RRContext : DbContext {
    
    public RRContext(DbContextOptions<RRContext> options) : base(options) { }

    public DbSet<Restroom> Restrooms { get; set; }    
  }
}