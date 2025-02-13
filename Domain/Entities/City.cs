using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class City
    {
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public Guid GovernorateId { get; set; }
        public Governorate Governorate { get; set; }
    }
}
