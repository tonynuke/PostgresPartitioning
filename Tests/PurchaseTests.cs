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

            person.AddPurchase();
            person.AddPurchase();

            await _dataBaseFixture.Context.Persons.AddAsync(person);
            await _dataBaseFixture.Context.SaveChangesAsync();

            var dbPerson = await _dataBaseFixture.Context.Persons
                .Where(p => p.Id == person.Id)
                .SingleAsync();

            dbPerson.Should().BeEquivalentTo(person);

            var purchase = new Purchase(person.Id, DateTime.UtcNow.AddYears(10));
            await _dataBaseFixture.Context.Purchases.AddAsync(purchase);
            await _dataBaseFixture.Context.SaveChangesAsync();

            var dbPurchases = _dataBaseFixture.Context.Purchases.ToList().Should().HaveCount(3);
        }
    }
}