using OnlineShop.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineShop.Services.Goods.Contracts
{
    public interface GoodRepository
    {
        void CheckForDuplicatedTitle(string title);
        bool IsDuplicatedCode(string code);
        Good Add(AddGoodDto dto);
        void Delete(int id);
        List<GetGoodDto> GetAll();
        Good Find(int id);
    }
}
