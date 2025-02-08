using System.ComponentModel.DataAnnotations;

namespace GameNightScoresRight.EFDTOs
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid AccountId { get; set; }

        [MaxLength(100)]
        public string? FirstName { get; set; }

        [MaxLength(100)]
        public string? LastName { get; set; }

        [Required, MaxLength(100)]
        public required string Username { get; set; }

        [MaxLength(20)]
        public string? PhoneNumber { get; set; }

        public int? Age { get; set; }

        [MaxLength(50)]
        public string? Pronouns { get; set; }

        [MaxLength(512)]
        public string? ProfilePictureUrl { get; set; }

        public string? Bio { get; set; }

        [Required]
        public DateTimeOffset CreatedAt { get; set; }

        [Required]
        public DateTimeOffset UpdatedAt { get; set; }

        [Required]
        public bool IsDeleted { get; set; }

        public Account? Account { get; set; }
    }
}
