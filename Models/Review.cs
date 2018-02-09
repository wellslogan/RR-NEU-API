using System;

namespace RR_NEU_API.Models 
{
  public class Review 
  {
    public int Id { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public int Rating { get; set; }

    public string Author { get; set; }

    public DateTime CreateDate { get; set; }

    public int RestroomId { get; set; }

    public Restroom Restroom { get; set; }

  }
}