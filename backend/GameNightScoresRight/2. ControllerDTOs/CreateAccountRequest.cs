using System.ComponentModel.DataAnnotations;

namespace GameNightScoresRight.ControllerDTOs
{
    public class CreateAccountRequest
    {
        [Required]
        public required string EmailAddress { get; set; }
        public int? Role { get; set; }
    }
}
