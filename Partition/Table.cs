using NodaTime;

namespace Partition
{
    public class Table
    {
        /// <summary>
        /// Название таблицы
        /// </summary>
        public required string TableName { get; init; }

        /// <summary>
        /// Партиции
        /// </summary>
        public required List<Partition> Partitions { get; init; }

        /// <summary>
        /// Возвращает следующую партицю
        /// </summary>
        /// <param name="date">Дата на которую нужно создать партицию</param>
        /// <param name="duration">Время действия партиции</param>
        /// <returns></returns>
        public Partition GetNextPartition(DateTime date, Duration duration)
        {
            var lastPartition = Partitions
                .OrderByDescending(partition => partition.Interval.End)
                .FirstOrDefault();
            if (lastPartition is null)
            {
                var intervalStart = Instant.FromDateTimeUtc(date);
                var interval = CreateInterval(intervalStart, duration);
                return Partition.Create(TableName, interval);
            }
            else
            {
                var intervalStart = lastPartition.Interval.End;
                var interval = CreateInterval(intervalStart, duration);
                return Partition.Create(TableName, interval);
            }
        }

        /// <summary>
        /// Возвращает необходимость создавать следуюущу партицию
        /// </summary>
        /// <param name="date">Дата на которую нужно создать партицию</param>
        /// <param name="dayBeforeCreation">
        /// За сколько дней до окончания действия существующей партиции создавать новую партицию
        /// </param>
        /// <returns></returns>
        public bool ShouldCreateNextPartition(DateTime date, int daysBeforeCreation)
        {
            var lastPartition = Partitions
                .OrderByDescending(partition => partition.Interval.End)
                .FirstOrDefault();
            if (lastPartition is null)
            {
                return true;
            }

            return date.AddDays(daysBeforeCreation) >= lastPartition.Interval.End.ToDateTimeUtc();
        }

        private static Interval CreateInterval(Instant intervalStart, Duration duration)
        {
            var intervalEnd = intervalStart.Plus(duration);
            return new Interval(intervalStart, intervalEnd);
        }
    }
}