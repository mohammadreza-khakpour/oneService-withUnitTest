using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineShop.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineShop.Persistence.EF.GoodCategories
{
    class GoodCategoryEntityMap : IEntityTypeConfiguration<GoodCategory>
    {
        public void Configure(EntityTypeBuilder<GoodCategory> builder)
        {
            builder.HasKey(_ => _.Id);
            builder.Property(_ => _.Title).HasMaxLength(50).IsRequired().IsUnicode();
        }
    }
}
