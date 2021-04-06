using System;
using System.Collections.Generic;

namespace OnlineShop.Entities
{
    public class GoodCategory
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public HashSet<Good> Goods { get; set; }
    }
}
