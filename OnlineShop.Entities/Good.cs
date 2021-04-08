using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OnlineShop.Entities
{
    public class Good
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Code { get; set; }
        [Required]
        public int MinimumAmount { get; set; }
        public int GoodCategoryId { get; set; }
        public GoodCategory GoodCategory { get; set; }
        public bool IsSufficientInStore { get; set; }
    }
}
