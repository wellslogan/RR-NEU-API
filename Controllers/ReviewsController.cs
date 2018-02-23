using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
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

        [HttpPost("add"), Authorize]
        public async Task<IActionResult> Add([FromBody]AddReviewRequest reviewRequest) 
        {
            if (reviewRequest == null || reviewRequest.Review == null) 
            {
                return BadRequest();
            }

            var review = reviewRequest.Review;

            if (string.IsNullOrEmpty(review.Title)) 
            {
                return Ok(new {Success = false});
            }

            var successfulCaptcha = await GoogleController.ValidateRecaptcha(reviewRequest.RecaptchaResponse);

            if (!successfulCaptcha) 
            {
                return Ok(new { Success = false});
            }

            // now that we've successfully passed all validations, get the Author id
            // from the User object and add the review
            var googleId = User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sub)?.Value;

            if (googleId == null) 
            {
                return BadRequest();
            }

            var author = await RRRepo.GetAuthorByGoogleId(googleId);
            var restroom = await RRRepo.GetById(review.RestroomId);

            review.Author = author;
            review.Restroom = restroom;

            await RRRepo.AddReview(review);
            return Ok(new {Success = true});
        }

    }
}
