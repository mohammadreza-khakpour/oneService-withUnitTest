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
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace OnlineShop.Services.Tests.Specs.Goods.Add
{
    public class FailedBecauseInvalidSufficiencyStatus
    {
        private int categoryId;
        private readonly EFInMemoryDatabase db = new EFInMemoryDatabase();
        private readonly EFDataContext context;
        private readonly GoodRepository goodRepository;
        private readonly UnitOfWork unitOfWork;
        private readonly GoodService sut;
        private Func<Task> actualSutResult;
        public FailedBecauseInvalidSufficiencyStatus()
        {
            context = db.CreateDataContext<EFDataContext>();
            goodRepository = new EFGoodRepository(context);
            unitOfWork = new EFUnitOfWork(context);
            sut = new GoodAppService(goodRepository, unitOfWork);
        }
        // Given[("یک دسته بندی به نام لبنیات در فهرست دسته بندی کالاها وجود دارد")]
        private void Given()
        {
            var category = new GoodCategory()
            {
                Title = "لبنیات"
            };
            context.Manipulate(_ => _.GoodCategories.Add(category));
            categoryId = category.Id;
        }
        // When[("کالایی با نام پنیر و بدون حداقل موجودی را در دسته بندی لبنیات، تعریف میکنم")]
        private void When()
        {
            var goodDto = new AddGoodDto()
            {
                Title = "پنیر",
                Code = "123456789000",
                GoodCategoryId = categoryId
            };

            actualSutResult = () => sut.Add(goodDto);

        }
        // Then[("باید یک خطا با مضمون "مشخص نبودن حداقل موجودی" دریافت کنم 
        //و کالا با نام پنیر  در فهرست کالاها وارد نشده باشد.")]
        private void Then()
        {
            actualSutResult.Should().Throw<InvalidSufficiencyStatusException>();
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
