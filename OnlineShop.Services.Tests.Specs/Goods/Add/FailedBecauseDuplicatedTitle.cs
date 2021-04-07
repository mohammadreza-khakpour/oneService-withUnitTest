using FluentAssertions;
using OnlineShop.Entities;
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
    public class FailedBecauseDuplicatedTitle
    {
        private readonly EFDataContext _context;
        private readonly GoodService _sut;
        private Good _good;
        private Func<Task> _expected;

        public FailedBecauseDuplicatedTitle()
        {
            var db = new EFInMemoryDatabase();
            _context = db.CreateDataContext<EFDataContext>();
            var unitOfWork = new EFUnitOfWork(_context);
            var repository = new EFGoodRepository(_context);
            _sut = new GoodAppService(repository, unitOfWork);
        }

        // فرض میکنم یک دسته بندی با عنوان لبنیات در فهرست دسته بندی کالا وجود دارد و کالایی
        // با نام ماست و کد 02 و حداقل موجودی 10 در فهرست کالاها و دسته لبنیات وجود دارد
        private void Given()
        {
            var goodCategory = new GoodCategory
            {
                Title = "لبنیات"
            };
            _context.Manipulate(_ => _.GoodCategories.Add(goodCategory));
            _good = new Good
            {
                Title = "mast",
                Code = "02",
                MinimumAmount = 10,
                GoodCategoryId = goodCategory.Id
            };
            _context.Manipulate(_ => _.Goods.Add(_good));
        }

        // زمانی که یک کالا با نام ماست و کد 03 و حداقل موجودی 20 در فهرست کالاهاودسته
        // لبنیات تعریف میکنم
        private void When()
        {
            var dto = new AddGoodDto
            {
                Title = "mast",
                Code = "03",
                MinimumAmount = 20,
                GoodCategoryId = _good.GoodCategoryId
            };
            _expected =  () => _sut.Add(dto);
        }

        // بنابرین باید خطای تکراری بودن ماست در دسته لبنیات نشان داده شود
        // و باید فقط یک کالا با عنوان ماست و کد 02 و حداقل موجودی 10 در
        // دسته لبنیات موجود باشد
        private void Then()
        {
            _expected.Should()
                .ThrowExactly<GoodDuplicatedTitleException>();
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
