﻿using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence
{
    internal sealed class PurchaseConfiguration
        : IEntityTypeConfiguration<Purchase>
    {
        public void Configure(EntityTypeBuilder<Purchase> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.DateTime);
            builder.Property(x => x.PersonId);

            builder.HasIndex(x => new { x.DateTime, x.PersonId });
        }
    }
}
