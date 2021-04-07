using OnlineShop.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineShop.Services.GoodCategories.Contracts
{
    public interface GoodCategoryRepository
    {
        void CheckForDuplicatedTitle(string title);
        GoodCategory Add(AddGoodCategoryDto dto);
    }
}
