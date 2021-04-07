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
        public void CheckForDuplicatedTitle(string title)
        {
            bool result = _dBContext.Goods
                .Any(good => good.Title == title);
            if (result == true)
            {
                throw new GoodDuplicatedTitleException();
            }
        }
        public void CheckForDuplicatedCode(string code)
        {
            bool result = _dBContext.Goods
                .Any(good => good.Code == code);
            if (result == true)
            {
                throw new GoodDuplicatedCodeException();
            }
        }

        public bool IsDuplicatedCode(string code)
        {
            bool result = _dBContext.Goods
                .Any(good => good.Code == code);
            return result;
        }
        public Good Add(AddGoodDto dto)
        {
            CheckForDuplicatedTitle(dto.Title);
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
        

        public void UpdateSufficiencyStatus(int goodId)
        {
            List<Warehouse> goodWarehouses =
                _dBContext.Warehouses.Where(x => x.GoodId == goodId).ToList();
            int goodOverallCount = 0;
            goodWarehouses.ForEach(x =>
            {
                goodOverallCount += x.GoodCount;
            });
            Good theGood = _dBContext.Goods.Find(goodId);
            if (theGood.MinimumAmount <= goodOverallCount)
            {
                theGood.IsSufficientInStore = false;
            }
            else
            {
                theGood.IsSufficientInStore = true;
            }
        }

        public void Delete(int id)
        {
            var res = Find(id);
            _dBContext.Goods.Remove(res);
        }

    }
}
