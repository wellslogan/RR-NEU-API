using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using RR_NEU_API.Repository;

namespace RR_NEU_API.Controllers
{
    [Route("api/[controller]")]
    public class BaseController : Controller 
    {
        protected IRRRepository RRRepo { get; set; }

        protected string GetUserGoogleId()
        {
            return User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sub)?.Value;
        }
    }
}