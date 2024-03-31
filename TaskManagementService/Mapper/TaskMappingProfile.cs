using AutoMapper;
using TaskManagementService.DTO;
using TaskManagementService.Model;

namespace TaskManagementService.Mapper
{
    public class TaskMappingProfile: Profile
    {
        public TaskMappingProfile()
        {
            CreateMap<TaskRequestDTO, TaskModel>();
        }
    }
}
