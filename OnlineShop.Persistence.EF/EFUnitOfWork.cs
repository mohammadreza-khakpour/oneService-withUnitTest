using OnlineShop.Infrastructure.Application;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineShop.Persistence.EF
{
    public class EFUnitOfWork : UnitOfWork
    {
        private EFDataContext _dBContext;
        public EFUnitOfWork(EFDataContext dBContext)
        {
            _dBContext = dBContext;
        }
        public void Complete()
        {
            _dBContext.SaveChanges();
        }
    }
}
