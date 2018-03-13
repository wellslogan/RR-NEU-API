using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RR_NEU_API.Models;
using RR_NEU_API.Contexts;
using RR_NEU_API.Extensions;

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

    public async Task AddReview(Review review) 
    {
      await _context.Reviews.AddAsync(review);
      await _context.SaveChangesAsync();
    }

    public async Task<IList<Restroom>> GetAll()
    {
        return await _context.Restrooms.Include(r => r.Reviews)//.ToListAsync();
        /*return rooms*/.Select(r => new Restroom {
                    Id = r.Id,
                    Description = r.Description,
                    Latitude = r.Latitude,
                    Longitude = r.Longitude,
                    Location = r.Location,
                    CreateDate = r.CreateDate,
                    Reviews = r.Reviews,
                    AverageRating = r.Reviews.Select(re => re.Rating).AverageOrNull()
                }
        ).ToListAsync();
    }

    public async Task<Restroom> GetById(int id)
    {
            return await _context.Restrooms
                                 .Include(r => r.Reviews)
                                    .ThenInclude(review => review.Author)
                                 .FirstOrDefaultAsync(r => r.Id == id);
    }

    public async Task<IList<Restroom>> Search(string q)
    {
      return await _context.Restrooms.Where(r => r.Description.ToLower().Contains(q.ToLower())).ToListAsync();
    }

    public async Task<Author> GetAuthorByGoogleId(string id) 
    {
        return await _context.Authors.FirstOrDefaultAsync(a => a.GoogleId == id);
    }

    public async Task AddAuthorAsync(Author a) 
    {
        await _context.Authors.AddAsync(a);
        await _context.SaveChangesAsync();
    }

    public async Task<IList<Review>> GetReviewsByAuthorId(int id) 
    {
        return await _context.Reviews
                             .Include(review => review.Restroom)
                             .Where(r => r.AuthorId == id).ToListAsync();
    }

    public async Task<bool> CheckAuthorOwnsReviewByGoogleId(int reviewId, string googleId)
    {
        var numberOfReviewsByTheAuthorWithThisGoogleId  = await _context.Reviews
            .Join(_context.Authors,
                    review => review.AuthorId, 
                    author => author.Id, 
                    (review, author) => new { Review = review, Author = author})
            .Where(reviewAndAuthor => 
                reviewAndAuthor.Author.GoogleId == googleId 
                && reviewAndAuthor.Review.Id == reviewId)
            .CountAsync();

        return numberOfReviewsByTheAuthorWithThisGoogleId == 1;
    }

    public async Task DeleteReviewById(int id)
    {
        var toRemove = await _context.Reviews.FindAsync(id);
        _context.Reviews.Remove(toRemove);
        await _context.SaveChangesAsync();
    }

  }
}