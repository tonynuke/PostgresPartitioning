using Domain;
using HotChocolate.Data;
using Persistence;

namespace WebService.GraphQL
{
    public class Query
    {
        [UseProjection]
        [UseSorting]
        [UseFiltering]
        public IQueryable<Person> GetPersons(
            [Service(ServiceKind.Synchronized)] ApplicationContext context)
            => context.Persons;

        [UseProjection]
        [UseSorting]
        [UseFiltering]
        public IQueryable<Purchase> GetPurchases(
            [Service(ServiceKind.Synchronized)] ApplicationContext context)
            => context.Purchases;
    }
}
