﻿using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineShop.Infrastructure.Application
{
    public interface UnitOfWork
    {
        public void Complete();
    }
}
