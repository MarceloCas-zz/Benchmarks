using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;
using Benchmarks.Dispose.Models;

namespace Benchmarks.Dispose.Benchs
{
    [SimpleJob(RunStrategy.Throughput, launchCount: 1)]
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
}
