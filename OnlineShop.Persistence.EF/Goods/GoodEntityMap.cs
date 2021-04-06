using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineShop.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineShop.Persistence.EF.Goods
{
    class GoodEntityMap : IEntityTypeConfiguration<Good>
    {
        public void Configure(EntityTypeBuilder<Good> builder)
        {
            builder.HasKey(_ => _.Id);
            builder.Property(_ => _.Title).IsUnicode().IsRequired().HasMaxLength(50);
            builder.Property(_ => _.Code).IsRequired().HasMaxLength(10);
            builder.Property(_ => _.MinimumAmount);
            builder.Property(_ => _.IsSufficientInStore).IsRequired().HasDefaultValue(false);
            builder.HasOne(_ => _.GoodCategory).WithMany(_ => _.Goods)
                .HasForeignKey(_ => _.GoodCategoryId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
