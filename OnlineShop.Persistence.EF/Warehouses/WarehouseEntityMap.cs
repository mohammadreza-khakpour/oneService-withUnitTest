using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineShop.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineShop.Persistence.EF.Warehouses
{
    class WarehouseEntityMap : IEntityTypeConfiguration<Warehouse>
    {
        public void Configure(EntityTypeBuilder<Warehouse> builder)
        {
            builder.HasKey(_ => _.Id);
            builder.Property(_ => _.GoodCount);
            builder.HasOne(_ => _.Good);
        }
    }
}
