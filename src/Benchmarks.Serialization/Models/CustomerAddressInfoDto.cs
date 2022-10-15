using Benchmarks.Serialization.Models.Base;

namespace Benchmarks.Serialization.Models;

public class CustomerAddressInfoDto
    : DtoBase
{
    // Properties
    public CustomerAddressDto[] CustomerAddressCollection { get; set; }
    public CustomerAddressDto? DefaultShippingAddressDto { get; set; }

    // Constructors
    public CustomerAddressInfoDto()
        : base()
    {
        CustomerAddressCollection = Array.Empty<CustomerAddressDto>();
    }

}
