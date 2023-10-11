using NodaTime;
using NodaTime.Text;
using System.Globalization;

namespace Partition
{
    /// <summary>
    /// Партиция
    /// </summary>
    public class Partition
    {
        private const string Separator = "__";
        private const string DateFormat = "yyyy_MM_dd";

        /// <summary>
        /// Название таблицы
        /// </summary>
        public required string TableName { get; init; }

        /// <summary>
        /// Название родительской таблицы
        /// </summary>
        public required string ParentTableName { get; init; }

        /// <summary>
        /// Временные границы партиции
        /// </summary>
        public required Interval Interval { get; init; }

        /// <summary>
        /// Парсит название партиции в объект
        /// </summary>
        /// <param name="tableName">Название партиции </param>
        /// <returns></returns>
        public static Partition Parse(string tableName)
        {
            var parts = tableName.Split(Separator);
            var parentTableName = parts[0];
            var intervalStart = ParseIntervalDate(parts[1]);
            var intervalEnd = ParseIntervalDate(parts[2]);

            var interval = new Interval(intervalStart, intervalEnd);

            return new Partition
            {
                TableName = tableName,
                ParentTableName = parentTableName,
                Interval = interval,
            };
        }

        /// <summary>
        /// Создает партицию
        /// </summary>
        /// <param name="parentTableName"></param>
        /// <param name="interval"></param>
        /// <returns></returns>
        public static Partition Create(string parentTableName, Interval interval)
        {
            var tableName = $"{parentTableName}{Separator}{IntervalToString(interval)}";

            return new Partition
            {
                TableName = tableName,
                ParentTableName = parentTableName,
                Interval = interval,
            };
        }

        private static Instant ParseIntervalDate(string intervalDate)
        {
            return InstantPattern.CreateWithInvariantCulture(DateFormat).Parse(intervalDate).Value;
        }

        private static string IntervalToString(Interval interval)
        {
            var start = interval.Start.ToString(DateFormat, CultureInfo.InvariantCulture);
            var end = interval.End.ToString(DateFormat, CultureInfo.InvariantCulture);
            return $"{start}{Separator}{end}";
        }
    }
}