using System;

namespace RR_NEU_API.Models 
{
  public class Review 
  {
    public int Id { get; set; }

    public string Description { get; set; }

    public int Rating { get; set; }

    public DateTime CreateDate { get; set; }

    public DateTime UpdateDate { get; set; }
  }
}