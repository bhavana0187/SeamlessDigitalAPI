using API.Models;
using ApplicationCore.Interfaces.Services;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using System.Security.Claims;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {        
        private readonly IConfiguration _config;
        private readonly bool _authorised;        
        private readonly IUnitOfWork _work;
        private readonly IMapper _mapper;
       
        
        public CategoryController(IUnitOfWork work, IMapper mapper, IConfiguration config, IHttpContextAccessor httpContextAccessor)
        {           
            _config = config;
            _work = work;     
            _authorised = false;
            _mapper = mapper;


            //For now set to true
            _authorised = true;
            //Check Basic authorization here and authorize--To Implement
            //if (httpContextAccessor.HttpContext.Request.Headers.TryGetValue("Authorization", out var value))
            //{
            //    //Check Basic authorization here and authorize--To Implement
            //    _authorised = true;
            //}
        }

        [HttpPost]
        [Route("AddCategory")]
        public async Task<ActionResult<int>> AddCategory(Category model)
        {

            if (!_authorised) return Unauthorized();          

            try
            {
                _work.CategoryRepo.Insert(_mapper.Map<Category,ApplicationCore.Models.Category>(model));
                _work.Save();
                model.Id = _work.CategoryRepo.GetLastInsertedId();
            }
            catch (Exception ex)
            {
                //log Failure                
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            finally
            {
              //log success
            }

            return new JsonResult(model.Id);
        }
    }
}
