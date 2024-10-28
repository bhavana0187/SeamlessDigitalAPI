using AutoMapper;

namespace API
{
    public class ObjectMappingProfileUnitTest : Profile
    {
        public ObjectMappingProfileUnitTest()
        {

            CreateMap<ApplicationCore.Models.Category, Models.Category>();
            CreateMap<Models.Category, ApplicationCore.Models.Category>();

            CreateMap<ApplicationCore.Models.ToDoItem, Models.ToDoItem>();
            CreateMap<Models.ToDoItem, ApplicationCore.Models.ToDoItem>();
 
        }
    }
}
