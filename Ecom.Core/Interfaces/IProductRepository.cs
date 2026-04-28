using Ecom.Core.Dtos;
using Ecom.Core.Entities.Product;
using Ecom.Core.Sharing;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecom.Core.Interfaces
{
    public interface IProductRepository:IGenericRepositores<Product>
    {
        // You can add any additional methods specific to Product repository here
        Task<IEnumerable<ProductDto>> GetAllAsync(ProductParam productParam);
        Task <bool> AddProductAsync(AddProductDto productDto);
        Task <bool> UpdateProductAsync(UpdateProductDto updateProductDto);
        Task DeleteProductAsync(Product product);
    }
}
