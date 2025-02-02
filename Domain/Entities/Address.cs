using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Address
    {
        public Guid Id { get; set; }
        [Required]
        public string Governorate { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Region { get; set; }
        public string PostalCode { get; set; }
    }
}
