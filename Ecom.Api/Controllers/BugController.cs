using AutoMapper;
using Ecom.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecom.Api.Controllers
{

    public class BugController : BaseController
    {
        public BugController(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {

        }
        [HttpGet("not-found")]
        public async Task<ActionResult> GetNotFound()
        {
            var category = await _unitOfWork.Categories.GetByIdAsync(999);
            if (category == null) return NotFound();
            return Ok(category);
        }
        [HttpGet("server-error")]
        public async Task<ActionResult> GetServerError()
        {
            var category = await _unitOfWork.Categories.GetByIdAsync(999);
            category.Name = "";
            return Ok(category);

        }
        [HttpGet("bad-request/{id}")]
        public async Task<ActionResult> GetBadRequest(int id)
        {
           
            return Ok();
        }
        [HttpGet("bad-request")]
        public async Task<ActionResult> GetBadRequest()
        {

            return BadRequest();
        }
    }
}
