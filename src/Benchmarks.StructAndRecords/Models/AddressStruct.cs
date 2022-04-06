namespace Benchmarks.StructAndRecords.Models
{
    public struct AddressStruct
    {
        public string Street { get; set; }
        public string Number { get; set; }
        public string ZipCode { get; set; }
        public string Neighborhood { get; set; }
        public string City { get; set; }

        public AddressStruct(string street, string number, string zipCode, string neighborhood, string city)
        {
            Street = street;
            Number = number;
            ZipCode = zipCode;
            Neighborhood = neighborhood;
            City = city;
        }
    }
}
