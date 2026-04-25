using AutoMapper;
using Ecom.Api.Helper;
using Ecom.Core.Dtos;
using Ecom.Core.Interfaces;
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
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var products = await _unitOfWork.Products.GetAllAsync(x => x.Category, x => x.Photos);
                var result = _mapper.Map<List<ProductDto>>(products);
                if (products == null)
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
    }
}
