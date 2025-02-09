using GameNightScoresRight.Enums;
using System.ComponentModel.DataAnnotations;

namespace GameNightScoresRight.ControllerDTOs
{
    public class CreateAccountRequest
    {
        [Required]
        public required string EmailAddress { get; set; }
        public Role? Role { get; set; }
    }
}
