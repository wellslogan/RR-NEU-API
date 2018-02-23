using System;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using RR_NEU_API.Repository;
using System.IdentityModel.Tokens.Jwt;

namespace RR_NEU_API.Controllers
{
	[Route("api/[controller]")]
	public class UsersController : Controller
    {
		public IRRRepository RRRepo { get; set; }

		public UsersController(IRRRepository _repo)
		{
			RRRepo = _repo;
		}

        [HttpGet("getCurrentUserReviews"), Authorize]
        public async Task<IActionResult> GetCurrentUserReviews() 
        {
            var googleId = User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sub)?.Value;

            if (googleId == null)
                return BadRequest();

            var author = await RRRepo.GetAuthorByGoogleId(googleId);

            if (author == null)
                return BadRequest();

            var reviews = await RRRepo.GetReviewsByAuthorId(author.Id);

            return Ok(reviews);
        }

        [HttpGet("findOrCreate"), Authorize]
        public async Task<IActionResult> FindOrCreate() 
        {
            var id = User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sub)?.Value;
            var name = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;

            if (id == null || name == null) {
                return BadRequest();
            }

            var author = await RRRepo.GetAuthorByGoogleId(id);

            if (author != null) 
            {
                return Ok(true);
            }

            author = new Models.Author()
            {
                GoogleId = id,
                Name = name
            };

            await RRRepo.AddAuthorAsync(author);

            return Ok(new
            {
                Success = true
            });
        }
    }
}
