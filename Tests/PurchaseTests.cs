using Domain;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Tests
{
    public class PurchaseTests : IClassFixture<DataBaseFixture>
    {
        private readonly DataBaseFixture _dataBaseFixture;

        public PurchaseTests(DataBaseFixture dataBaseFixture)
        {
            _dataBaseFixture = dataBaseFixture;
        }

        [Fact]
        public async Task Do_a_purchase()
        {
            var passport = Passport.Create("1000", "100000");
            var person = new Person(DateTime.UtcNow, passport.Value);

            person.AddPurchase("xxx");
            person.AddPurchase("yyy");

            using var context = _dataBaseFixture.CreateContext();
            await context.Persons.AddAsync(person);
            await context.SaveChangesAsync();

            var dbPerson = await context.Persons
                .Where(p => p.Id == person.Id)
                .SingleAsync();

            dbPerson.Should().BeEquivalentTo(person);

            var purchase = new Purchase(person.Id, DateTime.UtcNow.AddYears(10))
            {
                Comment = "Test"
            };
            await context.Purchases.AddAsync(purchase);
            await context.SaveChangesAsync();

            var dbPurchases = context.Purchases.ToList().Should().HaveCount(3);
        }
    }
}