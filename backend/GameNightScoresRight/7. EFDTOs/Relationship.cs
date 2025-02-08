using System.ComponentModel.DataAnnotations;

namespace GameNightScoresRight.EFDTOs
{
    public class Relationship
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid SenderId { get; set; }

        [Required]
        public Guid ReceiverId { get; set; }

        [Required]
        public int FriendStatus { get; set; }

        [Required]
        public DateTimeOffset CreatedAt { get; set; }

        [Required]
        public DateTimeOffset UpdatedAt { get; set; }

        [Required]
        public bool IsDeleted { get; set; }

        public User? Sender { get; set; }
        public User? Receiver { get; set; }
    }
}
