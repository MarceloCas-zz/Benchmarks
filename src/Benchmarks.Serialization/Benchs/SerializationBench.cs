using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Diagnosers;
using BenchmarkDotNet.Engines;
using Benchmarks.Serialization.Models;
using ProtoBuf.Meta;
using System.Text.Json;

namespace Benchmarks.Serialization.Benchs
{
    [SimpleJob(RunStrategy.Throughput)]
    [HardwareCounters(HardwareCounter.BranchMispredictions, HardwareCounter.BranchInstructions)]
    [MemoryDiagnoser]
    public class SerializationBench
    {
        private static bool _hasProtobufInitialized = false;

        [Params(100_000)]
        public int RunCount { get; set; }

        [Benchmark(Baseline = true, Description = "Json")]
        public void JsonSerialization()
        {
            var customerDto = CreateCustomerDto();

            for (int i = 0; i < RunCount; i++)
            {
                JsonSerializer.Serialize(customerDto);
            }
        }

        [Benchmark(Description = "Protobuf")]
        public void ProtobufSerialization()
        {
            var customerDto = CreateCustomerDto();

            var configProtobufTypeCollectionFunc = new Action<Type[]>(typeCollection =>
            {
                foreach (var type in typeCollection)
                {
                    var metaType = RuntimeTypeModel.Default.Add(
                        type,
                        applyDefaultBehaviour: false
                    );
                    var properties = type.GetProperties();
                    for (int i = 0; i < properties.Length; i++)
                    {
                        metaType.Add(properties[i].Name);
                    }
                }
            });

            if(!_hasProtobufInitialized)
            {
                configProtobufTypeCollectionFunc(new[]
                {
                    typeof(CustomerDto),
                    typeof(CustomerAddressInfoDto),
                    typeof(CustomerAddressDto)
                });

                _hasProtobufInitialized = true;
            }

            for (int i = 0; i < RunCount; i++)
            {
                using var memoryStream = new MemoryStream();
                RuntimeTypeModel.Default.Serialize(memoryStream, customerDto);
                var byteArray = memoryStream.ToArray();
            }
        }

        private static CustomerDto CreateCustomerDto()
        {
            var customerAddressDtoCollection = new CustomerAddressDto[5];
            for (int i = 0; i < customerAddressDtoCollection.Length; i++)
            {
                customerAddressDtoCollection[i] = new CustomerAddressDto
                {
                    Id = Guid.NewGuid(),
                    TenantId = Guid.NewGuid(),
                    CreatedBy = "marcelo.castelo@outlook.com",
                    CreatedAt = DateTime.UtcNow.AddDays(-1),
                    LastUpdatedBy = "marcelo.castelo@outlook.com",
                    LastUpdatedAt = DateTime.UtcNow,
                    LastSourcePlatform = "Benchmarks.Serialization",
                    RegistryVersion = DateTime.UtcNow,
                    CustomerAddressType = i,
                    Street = $"Street {i}",
                    Number = $"Number {i}",
                    City = $"City {i}",
                    State = $"State {i}",
                    Country = $"Country {i}",
                    ZipCode = $"ZipCode {i}",
                };
            }

            return new CustomerDto
            {
                Id = Guid.NewGuid(),
                TenantId = Guid.NewGuid(),
                CreatedBy = "marcelo.castelo@outlook.com",
                CreatedAt = DateTime.UtcNow.AddDays(-1),
                LastUpdatedBy = "marcelo.castelo@outlook.com",
                LastUpdatedAt = DateTime.UtcNow,
                LastSourcePlatform = "Benchmarks.Serialization",
                RegistryVersion = DateTime.UtcNow,
                FirstName = "Marcelo",
                LastName = "Castelo Branco",
                BirthDate = new DateTime(2000, 1, 1),
                Email = "marcelo.castelo@outlook.com",
                CustomerAddressInfo = new CustomerAddressInfoDto
                {
                    Id = Guid.NewGuid(),
                    TenantId = Guid.NewGuid(),
                    CreatedBy = "marcelo.castelo@outlook.com",
                    CreatedAt = DateTime.UtcNow.AddDays(-1),
                    LastUpdatedBy = "marcelo.castelo@outlook.com",
                    LastUpdatedAt = DateTime.UtcNow,
                    LastSourcePlatform = "Benchmarks.Serialization",
                    RegistryVersion = DateTime.UtcNow,
                    CustomerAddressCollection = customerAddressDtoCollection,
                    DefaultShippingAddressDto = customerAddressDtoCollection[0]
                }
            };
        }
    }
}
