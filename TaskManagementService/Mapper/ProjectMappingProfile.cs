using AutoMapper;
using TaskManagementService.DTO;
using TaskManagementService.Model;

namespace TaskManagementService.Mapper
{
    public class ProjectMappingProfile : Profile
    {
        public ProjectMappingProfile()
        {
            CreateMap<ProjectRequestDTO, ProjectModel>();
        }

    }
}
