using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Services.GoodCategories.Contracts
{
    public interface GoodCategoryService
    {
        Task<int> Add(AddGoodCategoryDto dto);
        GetGoodCategoryDto FindOneById(int id);
        List<GetGoodCategoryDto> GetAll();
    }
}
