using FluentAssertions;
using NodaTime;

namespace Partition.Tests
{
    public class PartitionParseTests
    {
        [Theory]
        [MemberData(nameof(TestCases))]
        public void Parse_partition(TestCase testCase)
        {
            var partition = Partition.Parse(testCase.TableName);
            partition.Should().BeEquivalentTo(testCase.Partition);
        }

        public static TheoryData<TestCase> TestCases = new()
        {
            new TestCase
            {
                TableName = "table__2000_09_01__2000_09_14",
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
            public required string TableName { get; init; }

            public required Partition Partition { get; init; }
        }
    }
}