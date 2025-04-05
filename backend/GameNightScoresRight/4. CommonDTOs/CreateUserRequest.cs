using System.ComponentModel.DataAnnotations;

namespace GameNightScoresRight.CommonDTOs
{
    public class CreateUserRequest
    {
        [Required]
        public string FirstName { get; set; } = string.Empty;

        public string? LastName { get; set; }

        [Required]
        public required string UserName { get; set; }

        public string? PhoneNumber { get; set; }

        public int? Age { get; set; }

        public string? Pronouns { get; set; }

        public string? ProfilePictureUrl { get; set; }

        public string? Bio { get; set; }
    }
}
