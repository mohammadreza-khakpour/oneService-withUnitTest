﻿using OnlineShop.Infrastructure.Application;
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
            bool duplicatedTitleCode = _goodRepository.IsDuplicatedCode(dto.Code);
            if (duplicatedTitleCode == true)
            {
                throw new GoodDuplicatedCodeException();
            }
            bool duplicatedTitleResult = _goodRepository.IsDuplicatedTitle(dto.Title);
            if (duplicatedTitleResult == true)
            {
                throw new GoodDuplicatedTitleException();
            }
            var record = _goodRepository.Add(dto);
            _unitOfWork.Complete();
            return record.Id;
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

        public List<GetGoodDto> GetAll()
        {
            return _goodRepository.GetAll();
        }

    }
}
