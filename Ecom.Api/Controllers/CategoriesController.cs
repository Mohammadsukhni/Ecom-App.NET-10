using AutoMapper;
using Ecom.Api.Helper;
using Ecom.Core.Dtos;
using Ecom.Core.Entities.Product;
using Ecom.Core.Interfaces;
using Ecom.Infrastructure.Repositries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Ecom.Core.Dtos.CategoryDto;

namespace Ecom.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : BaseController
    {
        public CategoriesController(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var categories = await _unitOfWork.Categories.GetAllAsync();
                if (categories == null)
                {
                    return BadRequest(new ResponseApi(400));
                }
                else
                    return Ok(categories);


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
                var category = await _unitOfWork.Categories.GetByIdAsync(id);
                if (category is null)
                {
                    return BadRequest(new ResponseApi(400,$"not found categroy id={id}"));
                }
                else
                    return Ok(category);
            }
            catch (Exception ex)
            {
                return BadRequest (ex.Message); 
            }
        }
        [HttpPost("add-category")]
        public async Task<IActionResult> AddCategory(CategoryDto categoryDto)
        {
            try
            {
                var category = _mapper.Map<Category>(categoryDto);
                await _unitOfWork.Categories.AddAsync(category);

                return Ok(new ResponseApi(200,"Item has been added"));

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("update-category")]
        public async Task<IActionResult> UpdateCategory(UpdateCategoryDto categoryDto)
        {
            try
            {
                var category = _mapper.Map<Category>(categoryDto);
                await _unitOfWork.Categories.UpdateAsync(category);
                return Ok(new ResponseApi(200, "Item has been updated"));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("delete-category/{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            try
            {
                await _unitOfWork.Categories.DeleteAsync(id);
                return Ok(new ResponseApi(200, "Item has been deleted"));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
