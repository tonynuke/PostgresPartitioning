using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence
{
    internal sealed class PersonConfiguration
        : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.HasKey(person => person.Id);

            builder.Property(person => person.BirthDate);

            //builder.HasMany(person => person.Purchases).WithOne().HasForeignKey(p => p.PersonId);

            //builder.OwnsMany(person => person.Purchases, purchase =>
            //{
            //    purchase.WithOwner().HasForeignKey(p => p.PersonId);
            //    purchase.Property(p => p.Price);
            //    purchase.Property(p => p.DateTime);
            //    purchase.Property(p => p.Id);
            //    purchase.HasKey(p => p.Id);
            //});

            builder.OwnsOne(person => person.Passport, passport =>
            {
                passport.Property(p => p.Series);
                passport.Property(p => p.Number);
            });
        }
    }
}
