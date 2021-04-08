using OnlineShop.Entities;
using OnlineShop.Services.Goods.Contracts;
using OnlineShop.Services.Goods.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OnlineShop.Persistence.EF.Goods
{
    public class EFGoodRepository : GoodRepository
    {
        private EFDataContext _dBContext;
        public EFGoodRepository(EFDataContext dBContext)
        {
            _dBContext = dBContext;
        }
        

        public bool IsDuplicatedCode(string code)
        {
            bool result = _dBContext.Goods
                .Any(good => good.Code == code);
            return result;
        }
        public Good Add(AddGoodDto dto)
        {
            bool res02 = IsDuplicatedTitle(dto.Title);
            if (res02 == true)
            {
                throw new GoodDuplicatedTitleException();
            }
            Good good = new Good
            {
                Code = dto.Code,
                MinimumAmount = dto.MinimumAmount,
                GoodCategoryId = dto.GoodCategoryId,
                Title = dto.Title,
            };
            var res = _dBContext.Goods.Add(good);
            return res.Entity;
        }

        public Good Find(int id)
        {
            return _dBContext.Goods.Find(id);
        }

        public void Delete(int id)
        {
            var res = Find(id);
            _dBContext.Goods.Remove(res);
        }

        public List<GetGoodDto> GetAll()
        {
            return _dBContext.Goods.Select(_ => new GetGoodDto
            {
                Id = _.Id,
                Title = _.Title,
                Code = _.Code,
                MinimumAmount = _.MinimumAmount,
                GoodCategoryId = _.GoodCategoryId,
                IsSufficientInStore = _.IsSufficientInStore
            }).ToList();
        }

        public bool IsDuplicatedTitle(string title)
        {
            bool result = _dBContext.Goods
                .Any(good => good.Title == title);
            return result;
        }

        public bool IsCodeLengthInvalid(string code)
        {
            return true ? code.Length > 10 : false;
        }

        public bool IsSufficiencyStatusInvalid(int minimumAmount)
        {
            if (minimumAmount==null || minimumAmount==0)
            {
                return true;
            }
            return false;
        }
    }
}
