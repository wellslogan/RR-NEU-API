using System;
using System.Collections.Generic;
using System.Linq;

namespace RR_NEU_API.Extensions 
{
  public static class RRLinqExtensions
  {
    public static Nullable<double> AverageOrNull(this IEnumerable<int> source)
    {
      if (source == null || !source.Any())
        return null;

      return source.Average();
    }
  }
}