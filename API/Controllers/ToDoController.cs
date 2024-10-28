using API.Models;
using ApplicationCore.Interfaces.Services;
using ApplicationCore.Interfaces.Services.Weather;

using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System.Net;
using System.Security.Claims;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly bool _authorised;
        private readonly IUnitOfWork _work;
        private readonly IMapper _mapper;
        private readonly IWeatherService _weatherService;
        public ToDoController(IUnitOfWork work, IMapper mapper, IConfiguration config, IWeatherService weatherService)
        {
            _config = config;
            _work = work;
            _authorised = false;
            _mapper = mapper;
            _weatherService = weatherService;

            //For now set authorized=true
            _authorised = true;
          

            //Check Basic authorization here and authorize
            //if (httpContextAccessor.HttpContext.Request.Headers.TryGetValue("Authorization", out var value))
            //{
            //    //Check Basic authorization here and authorize--To Implement
            //    _authorised = true;
            //}
        }

        [HttpPost]
        [Route("AddToDoItem")]
        public async Task<ActionResult<int>> AddToDoItem(ToDoItem model)
        {           
            if (!_authorised) return Unauthorized();          
            ToDoItemWithWeatherCondition returnValue = null;          
            try
            {
                _work.ToDoItemRepo.Insert(_mapper.Map<ToDoItem, ApplicationCore.Models.ToDoItem>(model));
                _work.Save();
                model.Id = _work.ToDoItemRepo.GetLastInsertedId();
                var toDoItem  = _mapper.Map<ApplicationCore.Models.ToDoItem, ToDoItem>(await _work.ToDoItemRepo.GetById(model.Id));

                //Get the weather condition if the Location is set 
                if(model.Location!=null && !String.IsNullOrWhiteSpace(model.Location.Latitude.ToString()) && !String.IsNullOrWhiteSpace(model.Location.Longitude.ToString()))
                {
                    var weatherResponse = await _weatherService.GetWeatherCondition(model.Location.Latitude, model.Location.Longitude);                    
                    if (weatherResponse != null)
                    {
                        returnValue = new ToDoItemWithWeatherCondition()
                        {
                            Id=toDoItem.Id,
                            ToDo=toDoItem.ToDo,
                            Priority=toDoItem.Priority,
                            Completed=toDoItem.Completed,
                             Category=toDoItem.Category,
                             Location=model.Location,
                             CurrentCondition=weatherResponse.current.condition.text,
                             CurrentTemperature=weatherResponse.current.temp_c
                        };
                    }
                }               

            }
            catch (Exception ex)
            {
                //log Failure here               
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            finally
            {
                //log success here
            }            
            return new JsonResult(returnValue);
        }
    
        [HttpGet]
        [Route("ToDoItemsSearchByTitleOrPriorityOrDueDate")]
        public async Task<ActionResult<IEnumerable<ToDoItem>>> GetToDoItemsSearchByTitleOrPriorityOrDueDate(string search)
        {            
            if (!_authorised) return Unauthorized();
            IEnumerable<ToDoItem> returnValue = null;
            try
            {
                var toDoItem=_work.ToDoItemRepo.GetToDoItemsSearchByTitleOrPriorityOrDueDate(search);
                returnValue = _mapper.Map<IEnumerable<ApplicationCore.Models.ToDoItem>, IEnumerable<ToDoItem>>(toDoItem);              
                
              
            }
            catch (Exception ex)
            {
                //log Failure here               
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            finally
            {
                //log success here
            }
            return new JsonResult(returnValue);
        }
      
        [HttpPut]
        [Route("UpdateToDoItem")]
        public async Task<ActionResult<ToDoItem>> UpdateToDoItem(ToDoItem model)
        {
            if (!_authorised) return Unauthorized();
            ToDoItem returnValue = null;
            try
            {
                _work.ToDoItemRepo.Update(_mapper.Map<ToDoItem, ApplicationCore.Models.ToDoItem>(model));
                _work.Save();

                var toDoItem = await _work.ToDoItemRepo.GetById(model.Id);
                returnValue = _mapper.Map<ApplicationCore.Models.ToDoItem, ToDoItem>(toDoItem);

            }
            catch (Exception ex)
            {
                //log Failure here               
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            finally
            {
                //log success here
            }

            return new JsonResult(returnValue);
        }
        
        [HttpGet]
        [Route("GetToDoItemById")]
        public async Task<ActionResult<Models.ToDoItem>> GetTodoItem(int id)
        {
            if (!_authorised) return Unauthorized();
            ToDoItem returnValue = null;
            try
            {
                var toDoItem = await _work.ToDoItemRepo.GetById(id);
                returnValue = _mapper.Map<ApplicationCore.Models.ToDoItem, ToDoItem>(toDoItem);


            }
            catch (Exception ex)
            {
                //log Failure here               
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            finally
            {
                //log success here
            }
            return new JsonResult(returnValue);
        }
        [HttpDelete]
        [Route("DeleteToDoItem")]
        public async Task<ActionResult> DeleteToDoItem(ToDoItem model)
        {
            if (!_authorised) return Unauthorized();
            try
            {
                _work.ToDoItemRepo.Delete(_mapper.Map<ToDoItem, ApplicationCore.Models.ToDoItem>(model));
                _work.Save();               
            }
            catch (Exception ex)
            {
                //log Failure here               
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            finally
            {
                //log success here
            }

            return NoContent();
        }

    }
}
