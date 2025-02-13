using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Governorate
    {
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        public List<City> Cities { get; set; } = new();
    }
}
