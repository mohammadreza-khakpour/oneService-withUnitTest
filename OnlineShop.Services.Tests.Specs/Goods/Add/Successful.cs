using FluentAssertions;
using OnlineShop.Entities;
using OnlineShop.Infrastructure.Test;
using OnlineShop.Persistence.EF;
using OnlineShop.Persistence.EF.Goods;
using OnlineShop.Services.Goods;
using OnlineShop.Services.Goods.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace OnlineShop.Services.Tests.Specs.Goods.Add
{
    public class Successful
    {
        GoodCategory goodCategory;
        EFDataContext context;
        int goodId;
        public Successful()
        {
            var db = new EFInMemoryDatabase();
            context = db.CreateDataContext<EFDataContext>();
        }
        //[Given("یک دسته بندی به نام لبنیات در فهرست دسته بندی کالاها وجود دارد")]
        private void Given()
        {
            goodCategory = new GoodCategory { Title = "لبنیات" };
            context.Manipulate(_ => _.GoodCategories.Add(goodCategory));
        }

        //[When("کالایی با نام ماست و کد 1006 و حداقل موجودی 10 را در دسته بندی لبنیات، تعریف میکنم")]
        private async void When()
        {
            var repository = new EFGoodRepository(context);
            var unitOfWork = new EFUnitOfWork(context);
            var sut = new GoodAppService(repository, unitOfWork);
            var dto = new AddGoodDto()
            {
                GoodCategoryId = goodCategory.Id,
                Code = "1006",
                MinimumAmount = 10,
                Title = "ماست"
            };
            goodId = await sut.Add(dto);
        }

        //[Then("باید یک کالا با نام ماست و کد 2و حداقل موجودی 10
        //در فهرست کالاهای با دسته بندی لبنیات وجود داشته باشد")]
        private void Then()
        {
            var expectd = context.Goods.Single(_ => _.Id == goodId);
            expectd.Title.Should().Be("ماست");
            expectd.GoodCategoryId.Should().Be(goodCategory.Id);
            expectd.Code.Should().Be("1006");
            expectd.MinimumAmount.Should().Be(10);
        }

        [Fact]
        public void Run()
        {
            Given();
            When();
            Then();
        }
    }
}
