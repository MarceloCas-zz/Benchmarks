namespace Benchmarks.StructAndRecords.Models
{
    public record struct AddressRecordStruct
    (
        string Street, 
        string Number, 
        string ZipCode, 
        string Neighborhood, 
        string City
    );
}
