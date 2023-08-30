using CSharpFunctionalExtensions;

namespace Domain
{
    public sealed class Purchase : Entity<Guid>
    {
        private Purchase()
        {
        }

        public Purchase(
            long personId, DateTime dateTime)
        {
            PersonId = personId;
            DateTime = dateTime;
 
        }

        public long PersonId { get; }

        public DateTime DateTime { get; }

        public string Comment { get; set; }
    }
}
