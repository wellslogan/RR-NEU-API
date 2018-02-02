using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RR_NEU_API.Models;
using RR_NEU_API.Repository;

namespace RR_NEU_API.Controllers
{
    [Route("api/[controller]")]
    public class RestroomsController : Controller
    {
        public IRRRepository RRRepo { get; set; }
        public RestroomsController(IRRRepository _repo)
        {
            RRRepo = _repo;
        }

        // GET api/restrooms
        [HttpGet]
        public async Task<IActionResult> Get() 
        {
            var result = await RRRepo.GetAll();
            return Ok(result);
        }

        // GET api/restrooms/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id) 
        {
            var result = await RRRepo.GetById(id);
            return Ok(result);
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody]Restroom restroom) 
        {
            if (restroom == null) 
            {
                return BadRequest();
            }

            if (string.IsNullOrEmpty(restroom.Description) 
                || string.IsNullOrEmpty(restroom.Latitude) 
                || string.IsNullOrEmpty(restroom.Longitude)) 
            {
                return Ok(new {Success = false});
            }

            await RRRepo.Add(restroom);
            return Ok(new {Success = true});
        }

    }
}
