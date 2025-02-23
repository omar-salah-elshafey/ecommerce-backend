using System.ComponentModel.DataAnnotations;

namespace Application.Features.Orders.Dtos
{
    public record CreateOrderDto(string PhoneNumber, Guid? AddressId, Guid? GovernorateId, Guid? CityId, string? Region);
}
