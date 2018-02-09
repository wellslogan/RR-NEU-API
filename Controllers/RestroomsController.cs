using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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

        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery]string q)
        {
            var result = await RRRepo.Search(q);
            return Ok(result);
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody]AddRestroomRequest restroomRequest) 
        {
            if (restroomRequest == null || restroomRequest.Restroom == null) 
            {
                return BadRequest();
            }

            var restroom = restroomRequest.Restroom;

            if (string.IsNullOrEmpty(restroom.Description) 
                || string.IsNullOrEmpty(restroom.Latitude) 
                || string.IsNullOrEmpty(restroom.Longitude)) 
            {
                return Ok(new {Success = false});
            }

            var successfulCaptcha = await GoogleController.ValidateRecaptcha(restroomRequest.RecaptchaResponse);

            if (!successfulCaptcha) 
            {
                return Ok(new { Success = false});
            }

            await RRRepo.Add(restroom);
            return Ok(new {Success = true});
        }

        

    }
}
