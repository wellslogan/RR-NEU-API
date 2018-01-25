using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RR_NEU_API.Models;

namespace RR_NEU_API.Controllers
{
    [Route("api/[controller]")]
    public class RestroomsController : Controller
    {
        // GET api/restrooms
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/restrooms/5
        [HttpGet("{id}")]
        public string GetRestroomById(int id)
        {
            return "value";
        }

        // POST api/restrooms
        [HttpPost]
        public void Post([FromForm]Restroom restroom)
        {
        }

    }
}
