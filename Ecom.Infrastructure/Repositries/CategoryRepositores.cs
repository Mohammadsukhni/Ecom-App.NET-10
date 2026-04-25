using Ecom.Core.Entities.Product;
using Ecom.Core.Interfaces;
using Ecom.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecom.Infrastructure.Repositries
{
    public class CategoryRepositores : GenericRepositories<Category>, ICategoryRepositores
    {
        public CategoryRepositores(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}
