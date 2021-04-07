using FluentAssertions;
using OnlineShop.Entities;
using OnlineShop.Infrastructure.Application;
using OnlineShop.Infrastructure.Test;
using OnlineShop.Persistence.EF;
using OnlineShop.Persistence.EF.Goods;
using OnlineShop.Services.Goods;
using OnlineShop.Services.Goods.Contracts;
using OnlineShop.Services.Goods.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace OnlineShop.Services.Tests.Unit.Goods
{
    public class GoodServiceTests
    {
        private UnitOfWork unitOfWork;
        private GoodRepository repository;
        private GoodService sut;
        private EFDataContext context;
        private EFDataContext readDataContext;

        public GoodServiceTests()
        {
            var db = new EFInMemoryDatabase();
            context = db.CreateDataContext<EFDataContext>();
            readDataContext = db.CreateDataContext<EFDataContext>();
            repository = new EFGoodRepository(context);
            unitOfWork = new EFUnitOfWork(context);
            sut = new GoodAppService(repository, unitOfWork);
        }

        [Fact]
        public async void Add_add_Good_properly()
        {
            var goodCategory = new GoodCategory()
            {
                Title = "dummy-title"
            };
            context.Manipulate(_ => _.GoodCategories.Add(goodCategory));
            var dto = GoodFactory.GenerateADDDto();
            dto.GoodCategoryId = goodCategory.Id;

            var actual = await sut.Add(dto);

            var expected = readDataContext.Goods.Single(_ => _.Id == actual);
            expected.Title.Should().Be(dto.Title);
        }

        [Fact]
        public void Add_prevent_add_when_code_exists()
        {
            var goodCategory = new GoodCategory()
            {
                Title = "dummy-title"
                
            };
            context.Manipulate(_ => _.GoodCategories.Add(goodCategory));
            var good = new Good()
            {
                Code = "11123",
                Title = "dummy-product-title",
                GoodCategoryId=goodCategory.Id
            };
            context.Manipulate(_ => _.Goods.Add(good));
            var dto = GoodFactory.GenerateADDDto();
            dto.Code = good.Code;

            Func<Task> expected = () => sut.Add(dto);


            expected.Should().Throw<GoodDuplicatedCodeException>();
        }

        [Fact]
        public void Add_prevent_add_when_title_exists()
        {
            var goodCategory = new GoodCategory()
            {
                Title = "dummy-title"

            };
            context.Manipulate(_ => _.GoodCategories.Add(goodCategory));
            var good = new Good()
            {
                Code = "111",
                Title = "dummy-product-title",
                GoodCategoryId = goodCategory.Id
            };
            context.Manipulate(_ => _.Goods.Add(good));
            var dto = GoodFactory.GenerateADDDto();
            dto.Title = good.Title;
            dto.Code = "222";

            Func<Task> expected = () => sut.Add(dto);

            expected.Should().Throw<GoodDuplicatedTitleException>();
        }

        [Fact]
        public async void Update_update_good_properly()
        {
            var goodCategory = new GoodCategory()
            {
                Title = "dummy-title"
            };
            context.Manipulate(_ => _.GoodCategories.Add(goodCategory));
            var dto = GoodFactory.GenerateADDDto();
            dto.GoodCategoryId = goodCategory.Id;
            int goodId = await sut.Add(dto);
            var updateDto = GoodFactory.GenerateUpdateDto();
            updateDto.GoodCategoryId = goodCategory.Id;

            int actual = await sut.Update(goodId,updateDto);

            var expected = readDataContext.Goods.Single(_ => _.Id == actual);
        }

        [Fact]
        public async void Delete_delete_good_properly()
        {
            var goodCategory = new GoodCategory()
            {
                Title = "dummy-title"
            };
            context.Manipulate(_ => _.GoodCategories.Add(goodCategory));
            var good = new Good()
            {
                Title = "dummy-good-title",
                Code = "dummy-good-code",
            };
            good.GoodCategoryId = goodCategory.Id;
            context.Manipulate(_ => _.Goods.Add(good));
            Good theGood = context.Goods.Single(_ => _.Title==good.Title);

            sut.Delete(theGood.Id);

            bool expected = context.Goods.Any(_ => _.Title == good.Title);
            expected.Should().BeFalse();
        }
    }
}
