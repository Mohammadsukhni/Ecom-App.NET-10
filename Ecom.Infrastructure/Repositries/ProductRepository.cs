using AutoMapper;
using Ecom.Core.Dtos;
using Ecom.Core.Entities.Product;
using Ecom.Core.Interfaces;
using Ecom.Core.Services;
using Ecom.Infrastructure.Data;
using Ecom.Infrastructure.Repositries.service;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecom.Infrastructure.Repositries
{
    public class ProductRepository : GenericRepositories<Product>, IProductRepository
    {
        private readonly AppDbContext dbContext;
        private readonly IMapper _mapper;
        private readonly IManageService _manageService;
        public ProductRepository(AppDbContext dbContext, IMapper mapper, IManageService manageService) : base(dbContext)
        {
            this.dbContext = dbContext;
            _mapper = mapper;
            _manageService = manageService;
        }
        public async Task<bool> AddProductAsync(AddProductDto productDto)
        {
            if (productDto == null)
            {
                return false;
            }
            var product = _mapper.Map<Product>(productDto);
            await dbContext.Products.AddAsync(product);
            await dbContext.SaveChangesAsync();
            var imagePath = await _manageService.AddImageAsync(productDto.Photo, productDto.Name);
            var photo = imagePath.Select(path => new Photo
            {
                ImageName = path,
                ProductId = product.Id
            }).ToList();
            await dbContext.Photos.AddRangeAsync(photo);
            await dbContext.SaveChangesAsync();
            return true;

        }
        public async Task<bool> UpdateProductAsync(UpdateProductDto updateProductDto)
        {
            if (updateProductDto == null)
            {
                return false;
            }
            var product = await dbContext.Products.Include(m => m.Category).Include(m => m.Photos).FirstOrDefaultAsync(p => p.Id == updateProductDto.Id);
            if (product == null)
                return false;
            _mapper.Map(updateProductDto, product);
            var findPhotos = await dbContext.Photos.Where(p => p.ProductId == updateProductDto.Id).ToListAsync();
            foreach (var item in findPhotos)
            {
                _manageService.DeleteImageAsync(item.ImageName);
            }
            dbContext.Photos.RemoveRange(findPhotos);
            var imagePath = await _manageService.AddImageAsync(updateProductDto.Photo, updateProductDto.Name);
            var photo = imagePath.Select(path => new Photo
            {
                ImageName = path,
                ProductId = product.Id
            }).ToList();
            await dbContext.Photos.AddRangeAsync(photo);
            await dbContext.SaveChangesAsync();
            return true;

        }
        public async Task DeleteProductAsync(Product product)
        {
            var photos = dbContext.Photos.Where(p => p.ProductId == product.Id).ToList();
            foreach (var item in photos)
            {
                _manageService.DeleteImageAsync(item.ImageName);
            }
            dbContext.Products.Remove(product);
            await dbContext.SaveChangesAsync();
        }
    }
}
