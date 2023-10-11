using FluentAssertions;
using NodaTime;

namespace Partition.Tests
{
    public class TableTests
    {
        private static readonly string _tableName = "table";

        [Theory]
        [MemberData(nameof(TestCases))]
        public void Create_next_partition(TestCase testCase)
        {
            var partition = testCase.Table.GetNextPartition(testCase.Date, testCase.Duration);
            partition.Should().BeEquivalentTo(testCase.NextPartition);

            testCase.Table.ShouldCreateNextPartition(testCase.Date, testCase.DaysBeforeCreation)
                .Should()
                .Be(testCase.ShouldCreateNextPartition);
        }

        public static TheoryData<TestCase> TestCases = new()
        {
            new TestCase
            {
                Table = new Table
                {
                    TableName = _tableName,
                    Partitions = new List<Partition>()
                    {
                        new Partition
                        {
                            Interval = new Interval(Instant.FromUtc(2000,9,1,0,0), Instant.FromUtc(2000,9,14,0,0)),
                            ParentTableName = _tableName,
                            TableName = $"{_tableName}__2000_09_01__2000_09_14"
                        },
                    }
                },
                Date = new DateTime(2000,9,13),
                Duration = Duration.FromDays(14),
                DaysBeforeCreation = 1,
                NextPartition = Partition.Create(
                    _tableName,
                    new Interval(Instant.FromUtc(2000,9,14,0,0), Instant.FromUtc(2000,9,28,0,0))),
               ShouldCreateNextPartition = true,
            }
        };

        public class TestCase
        {
            public required Table Table { get; init; }

            public required Partition NextPartition { get; init; }

            public required bool ShouldCreateNextPartition { get; init; }

            public required DateTime Date { get; init; }

            public required int DaysBeforeCreation { get; init; }

            public required Duration Duration { get; init; }
        }
    }
}
