using GameNightScoresRight.Enums;
using System.ComponentModel.DataAnnotations;

namespace GameNightScoresRight.EFDTOs
{
    public class Player
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid UserId { get; set; }

        [Required, MaxLength(100)]
        public required string Name { get; set; }

        [Required]
        public decimal Score { get; set; }

        public Guid? TeamId { get; set; }

        [Required]
        public Guid EventId { get; set; }

        public Position? Position { get; set; }

        [Required]
        public DateTimeOffset CreatedAt { get; set; }

        [Required]
        public DateTimeOffset UpdatedAt { get; set; }

        [Required]
        public bool IsDeleted { get; set; }

        public User? User { get; set; }
        public Team? Team { get; set; }
        public Event? Event { get; set; }
    }
}
