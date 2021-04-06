using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineShop.Entities
{
    public class Good
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Code { get; set; }
        public int MinimumAmount { get; set; }
        public int GoodCategoryId { get; set; }
        public GoodCategory GoodCategory { get; set; }
        public bool IsSufficientInStore { get; set; }
    }
}
