using Microsoft.EntityFrameworkCore;
using TaskManagementService.Data;
using TaskManagementService.Model;

namespace TaskManagementService.Services
{
    public class ProjectService: IProjectService
    {
       
        private readonly ApplicationDbContext _context;

        public ProjectService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ProjectModel>> GetAllProjectsAsync()
        {
            return await _context.Projects.ToListAsync();
        }

        public async Task<ProjectModel> GetProjectByIdAsync(int id)
        {
            return await _context.Projects.FindAsync(id);
        }

        public async Task<ProjectModel> CreateProjectAsync(ProjectModel project)
        {
            _context.Projects.Add(project);
            await _context.SaveChangesAsync();
            return project;
        }

        public async Task<bool> UpdateProjectAsync(int id, ProjectModel project)
        {
            var existingProject = await _context.Projects.FindAsync(id);
            if (existingProject == null)
                return false;

            existingProject.Name = project.Name;
            existingProject.Description = project.Description;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteProjectAsync(int id)
        {
            var project = await _context.Projects.FindAsync(id);
            if (project == null)
                return false;

            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();
            return true;
        }        
    }
}
