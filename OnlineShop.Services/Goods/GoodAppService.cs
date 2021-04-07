using OnlineShop.Infrastructure.Application;
using OnlineShop.Services.Goods.Contracts;
using OnlineShop.Services.Goods.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Services.Goods
{
    public class GoodAppService : GoodService
    {
        private GoodRepository _goodRepository;
        private UnitOfWork _unitOfWork;
        public GoodAppService(GoodRepository goodRepository, UnitOfWork unitOfWork)
        {
            _goodRepository = goodRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Add(AddGoodDto dto)
        {
            bool result = _goodRepository.IsDuplicatedCode(dto.Code);
            if (result == true)
            {
                throw new GoodDuplicatedCodeException();
            }
            CheckForDuplicatedTitle(dto.Title);
            var record = _goodRepository.Add(dto);
            _unitOfWork.Complete();
            return record.Id;
        }

        private void CheckForDuplicatedTitle(string title)
        {
            _goodRepository.CheckForDuplicatedTitle(title);
        }

        public void UpdateSufficiencyStatus(int goodId)
        {
            _goodRepository.UpdateSufficiencyStatus(goodId);
            _unitOfWork.Complete();
        }


        public async Task<int> Update(int id, UpdateGoodDto dto)
        {
            var res = _goodRepository.Find(id);
            res.Code = dto.Code;
            res.MinimumAmount = dto.MinimumAmount;
            res.GoodCategoryId = dto.GoodCategoryId;
            res.Title = dto.Title;
            res.IsSufficientInStore = dto.IsSufficientInStore;
            _unitOfWork.Complete();
            return res.Id;
        }

        public void Delete(int id)
        {
            _goodRepository.Delete(id);
            _unitOfWork.Complete();
        }

    }
}
