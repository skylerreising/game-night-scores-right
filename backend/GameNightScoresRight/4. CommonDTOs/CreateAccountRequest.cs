using GameNightScoresRight.Enums;
using System.ComponentModel.DataAnnotations;

namespace GameNightScoresRight.CommonDTOs
{
    public class CreateAccountRequest
    {
        [Required]
        public required string EmailAddress { get; set; }

        public Role? Role { get; set; }
    }
}