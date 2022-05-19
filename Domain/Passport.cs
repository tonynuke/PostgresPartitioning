using CSharpFunctionalExtensions;

namespace Domain
{
    public sealed class Passport : ValueObject
    {
        private Passport()
        {
        }

        private Passport(string series, string number)
        {
            Series = series;
            Number = number;
        }

        public string Series { get; private set; }

        public string Number { get; private set; }

        public static Result<Passport> Create(string series, string number)
        {
            List<string> errors = new List<string>();

            if (string.IsNullOrWhiteSpace(series))
            {
                errors.Add("Series can't be empty");
            }

            if (string.IsNullOrWhiteSpace(number))
            {
                errors.Add("Number can't be empty");
            }

            if (errors.Any())
            {
                var error = string.Join(", ", errors);
                return Result.Failure<Passport>(error);
            }

            return new Passport(series, number);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Series;
            yield return Number;
        }
    }
}
