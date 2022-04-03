using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;

namespace Benchmarks.Dispose.Benchs
{
    [SimpleJob(RunStrategy.Throughput, launchCount: 5)]
    [MemoryDiagnoser]
    public class DisposeBench
    {
        [Benchmark(Baseline = true)]
        public Guid WithoutDispose()
        {
            var customer = new Customer();
            return customer.Id;
        }
        [Benchmark]
        public Guid WithDispose()
        {
            using var customer = new CustomerWithDispose();
            return customer.Id;
        }
        [Benchmark]
        public Guid WithDisposeAndSuppressFinalize()
        {
            using var customer = new CustomerWithDisposeAndSuppressFinalize();
            return customer.Id;
        }
    }

    public enum CustomerTypeEnum
    {
        Undefinied = 0,
        Standard = 1,
        VIP = 2
    }

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
    public class CustomerWithDisposeAndSuppressFinalize
        : IDisposable
    {
        // Properties
        public Guid Id { get; }
        public string Name { get; }
        public CustomerTypeEnum CustomerType { get; }
        public DateTime DateOfBirth { get; }

        // Constructors
        public CustomerWithDisposeAndSuppressFinalize()
        {
            Id = Guid.NewGuid();
            Name = $"Customer {Id}";
            CustomerType = CustomerTypeEnum.Standard;
            DateOfBirth = DateTime.UtcNow;
        }

        // Public Methods
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
