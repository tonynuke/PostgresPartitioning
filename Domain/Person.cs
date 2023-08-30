using CSharpFunctionalExtensions;

namespace Domain
{
    public sealed class Person : Entity
    {
        private List<Purchase> _purchases = new();

        private Person()
        {
        }

        public Person(DateTime birthDate, Passport passport)
        {
            BirthDate = birthDate;
            Passport = passport;
        }

        public Person(long id, DateTime birthDate, Passport passport)
        {
            Id = id;
            BirthDate = birthDate;
            Passport = passport;
        }

        public DateTime BirthDate { get; private set; }

        public Passport Passport { get; private set; }

        public IReadOnlyCollection<Purchase> Purchases => _purchases;

        public void ChangePassport(Passport passport)
        {
            Passport = passport;
        }

        public Purchase AddPurchase(string comment)
        {
            var purchase = new Purchase(Id, DateTime.UtcNow)
            {
                Comment = comment,
            };
            _purchases.Add(purchase);

            return purchase;
        }
    }
}