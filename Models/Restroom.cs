using System;
using System.Collections.Generic;

namespace RR_NEU_API.Models
{
    public class Restroom 
    {
      public int Id { get; set; }

      public string Description { get; set; }

      public string Latitude { get; set; }
      
      public string Longitude { get; set; }

      public DateTime CreateDate { get; set; }

      public double? AverageRating { get; set; }

      public string Location { get; set; }

      public virtual List<Review> Reviews { get; set; }

    }
}