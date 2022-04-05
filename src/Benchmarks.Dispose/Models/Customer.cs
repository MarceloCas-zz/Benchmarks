namespace Benchmarks.Dispose.Models
{
    public class Customer
    {
        // Properties
        public Guid Id { get; }
        public string Name { get; }
        public CustomerTypeEnum CustomerType { get; }
        public DateTime DateOfBirth { get; }

        // Constructors
        public Customer()
        {
            Id = Guid.NewGuid();
            Name = $"Customer {Id}";
            CustomerType = CustomerTypeEnum.Standard;
            DateOfBirth = DateTime.UtcNow;
        }
    }
}
