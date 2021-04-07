using OnlineShop.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineShop.Services.Goods.Contracts
{
    public interface GoodRepository
    {
        void CheckForDuplicatedTitle(string title);
        void CheckForDuplicatedCode(string code);
        bool IsDuplicatedCode(string code);
        Good Add(AddGoodDto dto);
        void Delete(int id);
        Good Find(int id);
        void UpdateSufficiencyStatus(int goodId);
    }
}
