using Ecom.Core.Interfaces;
using Ecom.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecom.Infrastructure.Repositries
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _dbContext;
        public ICategoryRepositores Categories { get; }

        public IProductRepository Products { get; }

        public IPhotoRepository Photos { get; }
        public UnitOfWork(AppDbContext dbContext)
        {
            _dbContext = dbContext;
            Categories = new CategoryRepositores(_dbContext);
            Products = new ProductRepository(_dbContext);
            Photos = new PhotoRepository(_dbContext);
        }
    }
}
