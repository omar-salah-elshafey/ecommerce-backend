using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class BlogPost
    {
        public Guid Id { get; set; }
        [Required]
        [MaxLength(200)]
        public string Title { get; set; }
        [Required]
        [MaxLength(2000)]
        public string Content { get; set; }
        public DateTime PublishDate { get; set; }
        public string? ImageUrl { get; set; }
        public string? VideoUrl { get; set; }
        public int ReadTime { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
