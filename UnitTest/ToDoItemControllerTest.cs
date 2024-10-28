using API.Controllers;

using ApplicationCore.Interfaces.Data;
using ApplicationCore.Interfaces.Services;
using ApplicationCore.Models;
using AutoMapper;
using Infrastructure.Data;
using Infrastructure.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using API.Models;
using API;

namespace UnitTest
{
    public class ToDoItemControllerTest
    {     
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        private IWeatherService _weatherService;
        public ToDoItemControllerTest()
        { 
          
        }       

        [Fact]
        public async Task GetTodoItems_ReturnsTodoItems()
        {
            //Config
            var config = new ConfigurationBuilder()
                .SetBasePath(Environment.CurrentDirectory)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();
            IConfigurationRoot _config = config.Build();

            var options = new DbContextOptionsBuilder<SeamlessDigitalContext>()
                .UseInMemoryDatabase(databaseName: "SeamlessDigital")
                .Options;

            _weatherService = new WeatherService(_config);

            //Mapper
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new ObjectMappingProfileUnitTest());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            _mapper = mapper;


            using (var context = new SeamlessDigitalContext(options))
            {
                context.ToDoItems.Add(new ApplicationCore.Models.ToDoItem()
                { 
                  ToDo= "Do something nice for someone you care about",
                  Priority=2,
                  Completed=true,
                  UserId=152
                });
                context.SaveChanges();
            }

            using (var context = new SeamlessDigitalContext(options))
            {
                _unitOfWork = new UnitOfWork(context);
                var controller = new ToDoController(_unitOfWork,_mapper,_config,_weatherService);
                var result = await controller.GetToDoItemsSearchByTitleOrPriorityOrDueDate("category1");

                var okResult = Assert.IsType<OkObjectResult>(result.Result);
                var items = Assert.IsType<List<ApplicationCore.Models.ToDoItem>>(okResult.Value);
                Assert.Single(items);
            }
        }
    }
}