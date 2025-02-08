using System.ComponentModel.DataAnnotations;

namespace GameNightScoresRight.CommonDTOs
{
    public class CreateAccountRequest
    {
        [Required]
        public required string EmailAddress { get; set; }

        [Required]
        public int? Role { get; set; }
    }
}