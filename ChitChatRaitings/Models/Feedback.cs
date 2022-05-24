using System.ComponentModel.DataAnnotations;

namespace ChitChatRaitings.Models
{
    public class Feedback
    {
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        [Range(1,5)]
        public int Rate { get; set; }
        public string? Description { get; set; }

        public DateTime? CreatedDate { get; set; }

    }
}
