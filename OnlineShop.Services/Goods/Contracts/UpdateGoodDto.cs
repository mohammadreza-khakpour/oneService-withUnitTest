using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineShop.Services.Goods.Contracts
{
    public class UpdateGoodDto
    {
        public string Title { get; set; }
        public string Code { get; set; }
        public int MinimumAmount { get; set; }
        public int GoodCategoryId { get; set; }
        public bool IsSufficientInStore { get; set; }
        public UpdateGoodDto(string title, string code, int minimumAmount,
                                bool isSufficientInStore)
        {
            Title = title;
            Code = code;
            MinimumAmount = minimumAmount;
            IsSufficientInStore = isSufficientInStore;
        }
    }
}
