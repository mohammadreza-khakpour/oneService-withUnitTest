using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Services.Goods.Contracts
{
    public interface GoodService
    {
        Task<int> Add(AddGoodDto dto);
        Task<int> Update(int id, UpdateGoodDto dto);
        void Delete(int id);
        List<GetGoodDto> GetAll();
    }
}
