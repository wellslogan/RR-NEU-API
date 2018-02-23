using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace RR_NEU_API.Models
{
	public class Author
{
        public int Id { get; set; }

        [JsonIgnore]
        public string GoogleId { get; set; }

        public string Name { get; set; }

        public List<Review> Reviews { get; set; }
    }
}
