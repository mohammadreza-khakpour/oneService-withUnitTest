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

namespace OnlineShop.Services.Tests.Specs.Goods.Add
{
    public class FailedBecauseDuplicatedCode
    {
        private int categoryId;
        private readonly EFInMemoryDatabase db = new EFInMemoryDatabase();
        private readonly EFDataContext context;
        private readonly GoodRepository goodRepository;
        private readonly UnitOfWork unitOfWork;
        private readonly GoodService sut;
        private Func<Task> actualSutResult;
        public FailedBecauseDuplicatedCode()
        {
             context = db.CreateDataContext<EFDataContext>();
            goodRepository = new EFGoodRepository(context);
            unitOfWork = new EFUnitOfWork(context);
            sut = new GoodAppService(goodRepository,unitOfWork);
        }
        
        //Given[("یک دسته بندی به نام نوشیدنی های قندی در فهرست دسته بندی کالاها وجود دارد.
        //و کالایی با نام شانی  و کد 2  در فهرست کالاها وجود دارد")]
        //
        private void Given()
        {
            var category = new GoodCategory() { 
                Title="نوشیدنی های قندی"
            };
            
            context.Manipulate(_=>_.GoodCategories.Add(category));
            categoryId = category.Id;
            var good = new Good()
            {
                Title = "شانی",
                Code = "2",
                GoodCategoryId = categoryId
            };
            context.Manipulate(_=>_.Goods.Add(good));
        }
        // کالایی با نام پپسی قوطی و کد 2  را در دسته بندی نوشیدنی های قندی، تعریف میکنم
        private void When()
        {
            var goodDto = new AddGoodDto()
            {
                Title = "پپسی قوطی",
                Code = "2",
                GoodCategoryId = categoryId
            };

            actualSutResult = ()=>sut.Add(goodDto);
        }
        // باید یک خطا با مضمون "کد تکراری" دریافت کنم
        // و فقط یک کالا با نام شانی و کد 2  در فهرست کالاها وجود داشته باشد.
        private void then()
        {
            actualSutResult.Should().ThrowExactly<GoodDuplicatedCodeException>();
        }

        [Fact]
        public void Run()
        {
            Given();
            When();
            then();
        }
    }
}
