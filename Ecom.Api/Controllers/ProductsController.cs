using AutoMapper;
using Ecom.Api.Helper;
using Ecom.Core.Dtos;
using Ecom.Core.Entities.Product;
using Ecom.Core.Interfaces;
using Ecom.Core.Sharing;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecom.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : BaseController
    {
        public ProductsController(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }
        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll([FromQuery] ProductParam productParam)
        {
            try
            {
                var products = await _unitOfWork.Products.GetAllAsync(productParam);
                var totalCount=await _unitOfWork.Products.CountAsync();
                return Ok(new Pagination<ProductDto>(productParam.PageNumber, productParam.PageSize,totalCount,products));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("get-by-id/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var product = await _unitOfWork.Products.GetByIdAsync(id, x => x.Category, x => x.Photos);
                var result = _mapper.Map<ProductDto>(product);
                if (product == null)
                {
                    return BadRequest(new ResponseApi(400));
                }
                else
                    return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("add-product")]
        public async Task<IActionResult> AddProduct(AddProductDto productAddDto)
        {
            try
            {
                await _unitOfWork.Products.AddProductAsync(productAddDto);
                return Ok(new ResponseApi(200, "Product added successfully"));
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseApi(400, ex.Message));
            }
        }
        [HttpPut("update-product")]
        public async Task<IActionResult> UpdateProduct(UpdateProductDto productUpdateDto)
        {
            try
            {
                await _unitOfWork.Products.UpdateProductAsync(productUpdateDto);
                return Ok(new ResponseApi(200, "Product updated successfully"));
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseApi(400, ex.Message));
            }
        }
        [HttpDelete("delete-product/{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            try
            {
                var product = await _unitOfWork.Products.GetByIdAsync(id, x => x.Category, x => x.Photos);
                await _unitOfWork.Products.DeleteProductAsync(product);
                return Ok(new ResponseApi(200, "Product deleted successfully"));
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseApi(400, ex.Message));
            }
        }
    }
}
