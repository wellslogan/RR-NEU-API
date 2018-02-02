using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RR_NEU_API.Models;
using RR_NEU_API.Contexts;

namespace RR_NEU_API.Repository {
  public class RRRepository : IRRRepository
  {
    RRContext _context;
    public RRRepository(RRContext context)
    {
        _context = context;
    }

    public async Task Add(Restroom item)
    {
        await _context.Restrooms.AddAsync(item);
        await _context.SaveChangesAsync();
    }

    public async Task<IList<Restroom>> GetAll()
    {
        return await _context.Restrooms.ToListAsync();
    }

    public async Task<Restroom> GetById(int id)
    {
      return await _context.Restrooms.FirstOrDefaultAsync(r => r.Id == id);
    }
  }
}