using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RR_NEU_API.Models;
using RR_NEU_API.Contexts;

namespace RR_NEU_API.Repository {
  public interface IRRRepository
  {

    Task Add(Restroom item);

    Task AddReview(Review review);

    Task<IList<Restroom>> GetAll();

    Task<Restroom> GetById(int id);

    Task<IList<Restroom>> Search(string q);

    Task<Author> GetAuthorByGoogleId(string id);

    Task AddAuthorAsync(Author a);

    Task<IList<Review>> GetReviewsByAuthorId(int id);
  }
}