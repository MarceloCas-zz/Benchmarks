using Benchmarks.Serialization.Models.Base;

namespace Benchmarks.Serialization.Models;

public class CustomerDto
    : DtoBase
{
    // Properties
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime BirthDate { get; set; }
    public string Email { get; set; }
    public CustomerAddressInfoDto? CustomerAddressInfo { get; set; }

    // Constructors
    public CustomerDto()
        : base()
    {
        FirstName = LastName = Email = string.Empty;
    }
}
