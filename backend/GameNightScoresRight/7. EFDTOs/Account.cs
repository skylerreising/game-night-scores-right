using System.ComponentModel.DataAnnotations;

namespace GameNightScoresRight.EFDTOs
{
    public class Account
    {
        [Key]
        public Guid Id { get; set; }

        public Guid? UserId { get; set; }

        [Required, MaxLength(256)]
        public required string EmailAddress { get; set; }

        [Required]
        public int Role { get; set; }

        [Required]
        public DateTimeOffset CreatedAt { get; set; }

        [Required]
        public DateTimeOffset UpdatedAt { get; set; }

        [Required]
        public bool IsDeleted { get; set; }

        // Navigation property
        public User? User { get; set; }
    }
}