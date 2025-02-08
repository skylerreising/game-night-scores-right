using System.ComponentModel.DataAnnotations;


namespace GameNightScoresRight.EFDTOs
{
    public class Team
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid EventId { get; set; }

        [Required, MaxLength(100)]
        public required string Name { get; set; }

        [Required]
        public decimal Score { get; set; }

        [MaxLength(50)]
        public string? Color { get; set; }

        [MaxLength(100)]
        public string? Mascot { get; set; }

        public string? Description { get; set; }

        [MaxLength(512)]
        public string? LogoUrl { get; set; }

        public int? TeamType { get; set; }

        public int? MaxParticipants { get; set; }

        [Required]
        public DateTimeOffset CreatedAt { get; set; }

        [Required]
        public DateTimeOffset UpdatedAt { get; set; }

        [Required]
        public bool IsDeleted { get; set; }

        public Event? Event { get; set; }
    }
}
