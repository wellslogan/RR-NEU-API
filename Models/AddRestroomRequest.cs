namespace RR_NEU_API.Models 
{
  public class AddRestroomRequest 
  {
    public Restroom Restroom { get; set; }

    public string RecaptchaResponse { get; set; }
  }
}