using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace RR_NEU_API.Controllers {

  [Route("api/[controller]")]
  public class GoogleController : Controller 
  {
    [HttpGet("getLocationFromCoords")]
    public async Task<ActionResult> GetLocationFromCoords([FromQuery]string la, [FromQuery]string lo)
    {
      using (var client = new HttpClient()) 
      {
        try 
        {
          client.BaseAddress = new Uri("https://maps.googleapis.com/");
          var request = $"/maps/api/geocode/json?latlng={la},{lo}&key={Environment.GetEnvironmentVariable("GOOGLE_GEOCODING_API_KEY")}";

          var response = await client.GetAsync(request);
          response.EnsureSuccessStatusCode();

          var stringRes = await response.Content.ReadAsStringAsync();

          JObject jsonRes = JObject.Parse(stringRes);

          var address = (string)jsonRes["results"][0]["formatted_address"]; 

          return Ok(new { Address = address}); 
          // return Ok(new { Address = "Test Address"});
        } 
        catch (HttpRequestException ex) 
        {
          return BadRequest($"Error getting address {ex.Message}");
        }
      }
    }

  }

}