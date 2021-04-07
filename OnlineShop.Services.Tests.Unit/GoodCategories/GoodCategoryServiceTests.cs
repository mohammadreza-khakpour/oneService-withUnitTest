using FluentAssertions;
using OnlineShop.Entities;
using OnlineShop.Infrastructure.Application;
using OnlineShop.Infrastructure.Test;
using OnlineShop.Persistence.EF;
using OnlineShop.Persistence.EF.GoodCategories;
using OnlineShop.Services.GoodCategories;
using OnlineShop.Services.GoodCategories.Contracts;
using OnlineShop.Services.GoodCategories.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace OnlineShop.Services.Tests.Unit.GoodCategories
{
    public class GoodCategoryServiceTests
    {
        private UnitOfWork unitOfWork;
        private GoodCategoryRepository repository;
        private GoodCategoryService sut;
        private EFDataContext context;
        private EFDataContext readDataContext;

        public GoodCategoryServiceTests()
        {
            var db = new EFInMemoryDatabase();
            context = db.CreateDataContext<EFDataContext>();
            readDataContext = db.CreateDataContext<EFDataContext>();
            repository = new EFGoodCategoryRepository(context);
            unitOfWork = new EFUnitOfWork(context);
            sut = new GoodCategoryAppService(repository, unitOfWork);
        }

        [Fact]
        public async void Add_add_GoodCategory_properly()
        {
            var dto = GoodCategoryFactory.GenerateADDDto();

            var actual = await sut.Add(dto);

            var expected = readDataContext.GoodCategories.Single(_ => _.Id == actual);
            expected.Title.Should().Be(dto.Title);
        }

        [Fact]
        public void Add_prevent_add_when_title_exists()
        {
            var goodCategory = new GoodCategory()
            {
                Title = "dummy-title"
            };
            context.Manipulate(_ => _.GoodCategories.Add(goodCategory));
            var dto = GoodCategoryFactory.GenerateADDDto(goodCategory.Title);


            Func<Task> expected = () => sut.Add(dto);

            
            expected.Should().Throw<GoodCategoryDuplicatedTitleException>();
        }

    }
}
