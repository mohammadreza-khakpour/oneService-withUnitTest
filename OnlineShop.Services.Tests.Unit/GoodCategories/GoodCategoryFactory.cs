using OnlineShop.Services.GoodCategories.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineShop.Services.Tests.Unit.GoodCategories
{
    public static class GoodCategoryFactory
    {
        public static AddGoodCategoryDto GenerateADDDto(string title = "dummy-title")
        {
            return new AddGoodCategoryDto(title);
        }
    }
}
