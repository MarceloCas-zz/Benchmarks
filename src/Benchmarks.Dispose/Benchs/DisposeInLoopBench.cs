using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Diagnosers;
using BenchmarkDotNet.Engines;
using Benchmarks.Dispose.Models;

namespace Benchmarks.Dispose.Benchs
{
    [SimpleJob(RunStrategy.Throughput, launchCount: 1)]
    [HardwareCounters(HardwareCounter.BranchMispredictions, HardwareCounter.BranchInstructions)]
    [MemoryDiagnoser]
    public class DisposeInLoopBench
    {
        [Benchmark(Baseline = true)]
        public Guid WithoutDisposeInLoop()
        {
            var lastGuid = default(Guid);

            for (int i = 0; i < 1_000_000; i++)
            {
                var customer = new Customer();
                lastGuid = customer.Id;
            }

            return lastGuid;
        }
        [Benchmark]
        public Guid WithDisposeInLoop()
        {
            var lastGuid = default(Guid);

            for (int i = 0; i < 1_000_000; i++)
            {
                using var customer = new CustomerWithDispose();
                lastGuid = customer.Id;
            }

            return lastGuid;
        }
        [Benchmark]
        public Guid WithDisposeAndSuppressFinalizeInLoop()
        {
            var lastGuid = default(Guid);

            for (int i = 0; i < 1_000_000; i++)
            {
                using var customer = new CustomerWithDisposeAndSuppressFinalize();
                lastGuid = customer.Id;
            }

            return lastGuid;
        }
    }
}
