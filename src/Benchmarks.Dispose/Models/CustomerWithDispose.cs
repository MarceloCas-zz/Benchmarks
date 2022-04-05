namespace Benchmarks.Dispose.Models
{
    public class CustomerWithDispose
        : IDisposable
    {
        // Properties
        public Guid Id { get; }
        public string Name { get; }
        public CustomerTypeEnum CustomerType { get; }
        public DateTime DateOfBirth { get; }

        // Constructors
        public CustomerWithDispose()
        {
            Id = Guid.NewGuid();
            Name = $"Customer {Id}";
            CustomerType = CustomerTypeEnum.Standard;
            DateOfBirth = DateTime.UtcNow;
        }

        // Public Methods
        public void Dispose()
        {

        }
    }
}
