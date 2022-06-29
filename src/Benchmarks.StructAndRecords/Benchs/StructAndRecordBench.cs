using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Diagnosers;
using BenchmarkDotNet.Engines;
using Benchmarks.StructAndRecords.Models;

namespace Benchmarks.StructAndRecords.Benchs
{
    [SimpleJob(RunStrategy.Throughput, launchCount: 1)]
    [HardwareCounters(HardwareCounter.BranchMispredictions, HardwareCounter.BranchInstructions)]
    [MemoryDiagnoser]
    public class StructAndRecordBench
    {
        [Params(1, 1_000, 100_000, 1_000_000)]
        public int RunCount { get; set; }

        [Benchmark(Baseline = true)]
        public string? StructDeclaration()
        {
            var lastStreet = default(string);

            for (int i = 0; i < RunCount; i++)
            {
                var address = new AddressStruct(
                    street: string.Empty,
                    number: string.Empty,
                    zipCode: string.Empty,
                    neighborhood: string.Empty,
                    city: string.Empty
                );

                lastStreet = address.Street;
            }

            return lastStreet;
        }

        [Benchmark]
        public string? RecordDeclaration()
        {
            var lastStreet = default(string);

            for (int i = 0; i < RunCount; i++)
            {
                var address = new AddressRecord(
                    street: string.Empty,
                    number: string.Empty,
                    zipCode: string.Empty,
                    neighborhood: string.Empty,
                    city: string.Empty
                );

                lastStreet = address.Street;
            }

            return lastStreet;
        }

        [Benchmark]
        public string? RecordStructDeclaration()
        {
            var lastStreet = default(string);

            for (int i = 0; i < RunCount; i++)
            {
                var address = new AddressRecordStruct(
                    Street: string.Empty,
                    Number: string.Empty,
                    ZipCode: string.Empty,
                    Neighborhood: string.Empty,
                    City: string.Empty
                );

                lastStreet = address.Street;
            }

            return lastStreet;
        }
    }
}
