using RR_NEU_API.Models;

namespace RR_NEU_API.ViewModels 
{
  public class AddRestroomRequest 
  {
    public Restroom Restroom { get; set; }

    public string RecaptchaResponse { get; set; }
  }
}