using TaskManagementService.Model;

namespace TaskManagementService.Services
{
    public interface IProjectService
    {
        Task<IEnumerable<ProjectModel>> GetAllProjectsAsync();
        Task<ProjectModel> GetProjectByIdAsync(int id);
        Task<ProjectModel> CreateProjectAsync(ProjectModel project);
        Task<bool> UpdateProjectAsync(int id, ProjectModel project);
        Task<bool> DeleteProjectAsync(int id);
    }
}
