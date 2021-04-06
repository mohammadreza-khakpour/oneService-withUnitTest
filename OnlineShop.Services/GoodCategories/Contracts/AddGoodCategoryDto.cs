using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OnlineShop.Services.GoodCategories.Contracts
{
    public class AddGoodCategoryDto
    {
        [Required]
        public string Title { get; set; }
        public AddGoodCategoryDto(string title)
        {
            Title = title;
        }
    }
}
