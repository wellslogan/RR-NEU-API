using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using RR_NEU_API.Models;
using RR_NEU_API.Repository;

namespace RR_NEU_API.Controllers
{
    [Route("api/[controller]")]
    public class ReviewsController : Controller
    {
        public IRRRepository RRRepo { get; set; }
        public ReviewsController(IRRRepository _repo)
        {
            RRRepo = _repo;
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody]AddReviewRequest reviewRequest) 
        {
            if (reviewRequest == null || reviewRequest.Review == null) 
            {
                return BadRequest();
            }

            var review = reviewRequest.Review;

            if (string.IsNullOrEmpty(review.Title) 
                || string.IsNullOrEmpty(review.Author)) 
            {
                return Ok(new {Success = false});
            }

            await RRRepo.AddReview(review);
            return Ok(new {Success = true});
        }

    }
}
