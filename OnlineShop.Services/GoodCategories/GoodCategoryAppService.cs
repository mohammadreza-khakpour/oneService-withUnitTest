using OnlineShop.Infrastructure.Application;
using OnlineShop.Services.GoodCategories.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Services.GoodCategories
{
    public class GoodCategoryAppService : GoodCategoryService
    {
        private GoodCategoryRepository _goodCategoryRepository;
        private UnitOfWork _unitOfWork;
        public GoodCategoryAppService
            (GoodCategoryRepository goodCategoryRepository,
            UnitOfWork unitOfWork)
        {
            _goodCategoryRepository = goodCategoryRepository;
            _unitOfWork = unitOfWork;
        }
        private void CheckForDuplicatedTitle(string title)
        {
            _goodCategoryRepository.CheckForDuplicatedTitle(title);
        }
        public async Task<int> Add(AddGoodCategoryDto dto)
        {
            CheckForDuplicatedTitle(dto.Title);
            var record = _goodCategoryRepository.Add(dto);
            _unitOfWork.Complete();
            return record.Id;
        }
        public List<GetGoodCategoryDto> GetAll()
        {
            return _goodCategoryRepository.GetAll();
        }
        public GetGoodCategoryDto FindOneById(int id)
        {
            return _goodCategoryRepository.FindOneById(id);
        }
    }
}
