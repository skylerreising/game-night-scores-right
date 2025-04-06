using System.ComponentModel.DataAnnotations;

namespace GameNightScoresRight.ControllerDTOs
{
    public class CreateUserRequest
    {
        [Required]
        public required Guid UserId { get; set; }

        [Required]
        public required string FirstName { get; set; }

        public string? LastName { get; set; }

        [Required]
        public required string Username { get; set; }

        public string? PhoneNumber { get; set; }

        public int? Age { get; set; }

        public string? Pronouns { get; set; }

        public string? ProfilePictureUrl { get; set; }

        public string? Bio { get; set; }
    }
}
