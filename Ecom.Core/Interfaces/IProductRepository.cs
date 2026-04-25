using Ecom.Core.Entities.Product;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecom.Core.Interfaces
{
    public interface IProductRepository:IGenericRepositores<Product>
    {
        // You can add any additional methods specific to Product repository here
    }
}
