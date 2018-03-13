using RR_NEU_API.Models;

namespace RR_NEU_API.ViewModels 
{
  public class AddReviewRequest 
  {
    public Review Review { get; set; }

    public string RecaptchaResponse { get; set; }
  }
}