using System;

namespace RR_NEU_API.Models
{
    public class Restroom 
    {
      public int Id { get; set; }

      public string Description { get; set; }

      public string Latitude { get; set; }
      
      public string Longitude { get; set; }

      public DateTime CreateDate { get; set; }

    }
}