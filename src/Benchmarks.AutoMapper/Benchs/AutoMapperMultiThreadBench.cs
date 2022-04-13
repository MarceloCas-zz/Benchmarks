using AutoMapper;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Diagnosers;
using BenchmarkDotNet.Engines;
using Benchmarks.AutoMapper.Models;
using Microsoft.Extensions.DependencyInjection;

namespace Benchmarks.AutoMapper.Benchs
{
    [SimpleJob(RunStrategy.Throughput, launchCount: 1)]
    [HardwareCounters(HardwareCounter.BranchMispredictions, HardwareCounter.BranchInstructions)]
    [MemoryDiagnoser]
    public class AutoMapperMultiThreadBench
    {
        [Params(1, 10, 100, 1000)]
        public int RunCount { get; set; }

        public IMapper CreateMapper()
        {
            return new MapperConfiguration(q => {
                q.CreateMap<AddressDto, Address>();
            }).CreateMapper();
        }
        public AddressDto CreateAddressDto()
        {
            return new AddressDto
            {
                City = "São Paulo",
                Neighborhood = "Se",
                Number = "N/A",
                Street = "Praça da Sé",
                ZipCode = "01001-000"
            };
        }


        [Benchmark(Baseline = true)]
        public Address? MultiThread_SingleMapperInstance()
        {
            var mapper = CreateMapper();
            var lastAddress = default(Address);

            var parallelForResult = Parallel.For(0, RunCount, i => {
                var address = mapper.Map<AddressDto, Address>(CreateAddressDto());
                lastAddress = address;
            });

            while (!parallelForResult.IsCompleted)
            {

            }

            return lastAddress;
        }

        [Benchmark()]
        public Address? MultiThread_MultiMapperInstance()
        {
            var lastAddress = default(Address);

            var parallelForResult = Parallel.For(0, RunCount, i => {
                var mapper = CreateMapper();
                var address = mapper.Map<AddressDto, Address>(CreateAddressDto());
                lastAddress = address;
            });

            while (!parallelForResult.IsCompleted)
            {

            }

            return lastAddress;
        }

        [Benchmark()]
        public Address? MultiThread_SingleMapperInstanceWithIoC()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddAutoMapper(q => q.CreateMap<AddressDto, Address>());
            var serviceProvider = serviceCollection.BuildServiceProvider();

            var lastAddress = default(Address);
            var mapper = serviceProvider.CreateScope().ServiceProvider.GetService<IMapper>();

            var parallelForResult = Parallel.For(0, RunCount, i => {
                var address = mapper.Map<AddressDto, Address>(CreateAddressDto());
                lastAddress = address;
            });

            while (!parallelForResult.IsCompleted)
            {

            }

            return lastAddress;
        }

        [Benchmark()]
        public Address? MultiThread_MultiMapperInstanceWithIoC()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddAutoMapper(q => q.CreateMap<AddressDto, Address>());
            var serviceProvider = serviceCollection.BuildServiceProvider();

            var lastAddress = default(Address);

            var parallelForResult = Parallel.For(0, RunCount, i => {
                var mapper = serviceProvider.CreateScope().ServiceProvider.GetService<IMapper>();
                var address = mapper.Map<AddressDto, Address>(CreateAddressDto());
                lastAddress = address;
            });

            while (!parallelForResult.IsCompleted)
            {

            }

            return lastAddress;
        }
    }
}
