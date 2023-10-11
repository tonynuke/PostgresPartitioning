using FluentAssertions;
using NodaTime;

namespace Partition.Tests
{
    public class PartitionCreateTests
    {
        [Theory]
        [MemberData(nameof(TestCases))]
        public void Create_partition(TestCase testCase)
        {
            var partition = Partition.Create(testCase.ParentTableName, testCase.Partition.Interval);
            partition.Should().BeEquivalentTo(testCase.Partition);
        }

        public static TheoryData<TestCase> TestCases = new()
        {
            new TestCase
            {
                ParentTableName = "table",
                Partition = new Partition
                {
                    Interval = new Interval(Instant.FromUtc(2000,9,1,0,0), Instant.FromUtc(2000,9,14,0,0)),
                    ParentTableName = "table",
                    TableName = "table__2000_09_01__2000_09_14"
                },
            }
        };

        public class TestCase
        {
            public required string ParentTableName { get; init; }

            public required Partition Partition { get; init; }
        }
    }
}