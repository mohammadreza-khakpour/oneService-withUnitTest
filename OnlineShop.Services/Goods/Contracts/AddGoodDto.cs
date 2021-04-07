using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineShop.Services.Goods.Contracts
{
    public class AddGoodDto
    {
        public string Title { get; set; }
        public string Code { get; set; }
        public int MinimumAmount { get; set; }
        public int GoodCategoryId { get; set; }

        public AddGoodDto(string title, string code)
        {
            Title = title;
            Code = code;
        }
    }
}
