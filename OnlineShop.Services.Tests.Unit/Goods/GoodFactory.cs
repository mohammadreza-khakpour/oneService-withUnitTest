using OnlineShop.Services.Goods.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineShop.Services.Tests.Unit.Goods
{
    public static class GoodFactory
    {
        public static AddGoodDto GenerateADDDto(string title = "dummy-title", string code = "111")
        {
            return new AddGoodDto(title,code);
        }

        internal static UpdateGoodDto GenerateUpdateDto(string title="dummy-title-02", string code="222", 
                                                int minimumAmount = 10, bool isSufficientInStore=false)
        {
            return new UpdateGoodDto(title,code,minimumAmount,isSufficientInStore);
        }
    }
}
