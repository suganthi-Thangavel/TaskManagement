using TaskManagementService.Model;

namespace TaskManagementService.Services
{
    public interface ITaskService
    {
        Task<TaskModel> GetTaskById(int id);
        Task<TaskModel> CreateTask(TaskModel task);
        Task<IEnumerable<TaskModel>> GetAllTasksAsync();
    }
}
