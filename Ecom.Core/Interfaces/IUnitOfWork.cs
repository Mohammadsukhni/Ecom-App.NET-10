using System;
using System.Collections.Generic;
using System.Text;

namespace Ecom.Core.Interfaces
{
    public interface IUnitOfWork
    {
        public ICategoryRepositores Categories { get; }
        public IProductRepository Products { get; }
        public IPhotoRepository Photos { get; }
    }
}
