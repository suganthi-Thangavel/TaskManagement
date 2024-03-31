using Microsoft.EntityFrameworkCore;
using TaskManagementService.Data;
using TaskManagementService.Model;

namespace TaskManagementService.Services
{
    public class TaskService: ITaskService
    {
        private readonly ApplicationDbContext _context;

        public TaskService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TaskModel>> GetAllTasksAsync()
        {
            return await _context.Tasks.ToListAsync();
        }

        public async Task<TaskModel> CreateTask(TaskModel task)
        {
            try
            {
                _context.Tasks.Add(task); // Add the task to DbSet
                await _context.SaveChangesAsync(); // Save changes to the database
                return task; // Return the created task
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<TaskModel> GetTaskById(int id)
        {
            return await _context.Tasks.FirstOrDefaultAsync(t => t.Id == id);
        }
    }
}
