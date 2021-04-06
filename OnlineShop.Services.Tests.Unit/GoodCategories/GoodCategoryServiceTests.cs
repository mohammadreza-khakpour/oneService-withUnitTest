using FluentAssertions;
using OnlineShop.Infrastructure.Application;
using OnlineShop.Infrastructure.Test;
using OnlineShop.Persistence.EF;
using OnlineShop.Persistence.EF.GoodCategories;
using OnlineShop.Services.GoodCategories;
using OnlineShop.Services.GoodCategories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace OnlineShop.Services.Tests.Unit.GoodCategories
{
    public class GoodCategoryServiceTests
    {
        [Fact]
        public async void Add_add_GoodCategory_properly()
        {
            var db = new EFInMemoryDatabase();
            EFDataContext context = db.CreateDataContext<EFDataContext>();
            EFDataContext readDataContext = db.CreateDataContext<EFDataContext>();
            GoodCategoryRepository repository = new EFGoodCategoryRepository(context);
            UnitOfWork unitOfWork = new EFUnitOfWork(context);
            GoodCategoryService sut = new GoodCategoryAppService(repository, unitOfWork);
            var dto = GoodCategoryFactory.GenerateADDDto();

            var actual = await sut.Add(dto);

            var expected = readDataContext.GoodCategories.Single(_ => _.Id == actual);
            expected.Title.Should().Be(dto.Title);
        }
    }
}
