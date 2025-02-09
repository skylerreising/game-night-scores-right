using GameNightScoresRight.Enums;
using System.ComponentModel.DataAnnotations;

namespace GameNightScoresRight.EFDTOs
{
    public class Event
    {
        [Key]
        public Guid Id { get; set; }

        [Required, MaxLength(100)]
        public required string Name { get; set; }

        public DateTimeOffset? StartTime { get; set; }

        public DateTimeOffset? EndTime { get; set; }

        [MaxLength(100)]
        public string? LocationName { get; set; }

        [MaxLength(256)]
        public string? LocationAddress { get; set; }

        [MaxLength(256)]
        public string? LocationWebsite { get; set; }

        public string? Notes { get; set; }

        public EventType? EventType { get; set; }

        public EventStatus? Status { get; set; }

        public int? MaxParticipants { get; set; }

        [Required]
        public DateTimeOffset CreatedAt { get; set; }

        [Required]
        public DateTimeOffset UpdatedAt { get; set; }

        [Required]
        public bool IsDeleted { get; set; }
    }
}
