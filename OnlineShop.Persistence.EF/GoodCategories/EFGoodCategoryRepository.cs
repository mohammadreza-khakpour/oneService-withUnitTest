using OnlineShop.Entities;
using OnlineShop.Services.GoodCategories.Contracts;
using OnlineShop.Services.GoodCategories.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OnlineShop.Persistence.EF.GoodCategories
{
    public class EFGoodCategoryRepository : GoodCategoryRepository
    {
        private EFDataContext _dBContext;
        public EFGoodCategoryRepository(EFDataContext dBContext)
        {
            _dBContext = dBContext;
        }
        public void CheckForDuplicatedTitle(string title)
        {
            bool result = _dBContext.GoodCategories
                .Any(goodCategory => goodCategory.Title == title);
            if (result == true)
            {
                throw new GoodCategoryDuplicatedTitleException();
            }
        }
        public GoodCategory Add(AddGoodCategoryDto dto)
        {
            var res = _dBContext.GoodCategories.Add(new GoodCategory
            {
                Title = dto.Title
            });
            return res.Entity;
        }
        public List<GetGoodCategoryDto> GetAll()
        {
            return _dBContext.GoodCategories.Select(_ => new GetGoodCategoryDto
            {
                Id = _.Id,
                Title = _.Title
            }).ToList();
        }

        public GetGoodCategoryDto FindOneById(int id)
        {
            var theGoodCategory = _dBContext.GoodCategories.Find(id);
            return new GetGoodCategoryDto() { Id = theGoodCategory.Id, Title = theGoodCategory.Title };
        }
    }
}
