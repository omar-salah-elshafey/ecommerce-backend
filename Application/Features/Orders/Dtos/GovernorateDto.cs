namespace Application.Features.Orders.Dtos
{
    public class GovernorateDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<CityDto> Cities { get; set; }
    }
}
