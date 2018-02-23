using System;
using Newtonsoft.Json;

namespace RR_NEU_API.Models 
{
  public class Review 
  {
    public int Id { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public int Rating { get; set; }

    public DateTime CreateDate { get; set; }

    [JsonIgnore]
    public int RestroomId { get; set; }

    [JsonIgnore]
    public int AuthorId { get; set; }

    [JsonIgnore]
    public bool AuthorIsAnonymous { get; set; }

    public Restroom Restroom { get; set; }

    public Author Author { get; set; }

  }
}