namespace RR_NEU_API.Models 
{
  public class AddReviewRequest 
  {
    public Review Review { get; set; }

    public string RecaptchaResponse { get; set; }
  }
}