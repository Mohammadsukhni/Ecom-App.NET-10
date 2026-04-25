using AutoMapper;
using Ecom.Core.Interfaces;
using Ecom.Core.Services;
using Ecom.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecom.Infrastructure.Repositries
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IManageService _manageService;
        public ICategoryRepositores Categories { get; }

        public IProductRepository Products { get; }

        public IPhotoRepository Photos { get; }
        public UnitOfWork(AppDbContext dbContext, IMapper mapper, IManageService manageService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _manageService = manageService;
            Categories = new CategoryRepositores(_dbContext);
            Products = new ProductRepository(_dbContext, _mapper, _manageService);
            Photos = new PhotoRepository(_dbContext);
            
        }
    }
}
