using AutoMapper;
using Azure.Core;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManagementService.Data;
using TaskManagementService.DTO;
using TaskManagementService.Model;
using TaskManagementService.Services;


namespace TaskManagementService.Controllers
{
    [Route("api/tasks")]
    [ApiController]
    [EnableCors("AllowAnyOrigin")]
    public class TasksController : ControllerBase
    {
        private readonly ITaskService _taskService;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IMapper _mapper;
        public TasksController(ITaskService taskService, IWebHostEnvironment hostingEnvironment, IMapper mapper)
        {
            _taskService = taskService;
            _hostingEnvironment = hostingEnvironment;
            _mapper = mapper;
        }

        [HttpGet]       
        public async Task<ActionResult<TaskModel>> GetTask()
        {
            var task = await _taskService.GetAllTasksAsync();
            if (task == null)
            {
                return NotFound();
            }
            return Ok(task);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<TaskModel>> GetTask(int id)
        {
            var task = await _taskService.GetTaskById(id);
            if (task == null)
            {
                return NotFound();
            }
            return Ok(task);
        }

        [HttpPost]
        [Route("Add")]
        public async Task<ActionResult<Task>> CreateTask([FromForm] TaskRequestDTO request)
        {
            try
            {
                var taskModel = _mapper.Map<TaskModel>(request);
                if (taskModel != null)
                {
                    var attachmentPaths = new List<string>();
                    if (request.Attachments != null)
                    {
                        foreach (var attachment in request.Attachments)
                        {
                            var filePath = Path.Combine(_hostingEnvironment.ContentRootPath, "FileStorage", attachment.FileName);
                            attachmentPaths.Add(filePath);

                            using (var stream = new FileStream(filePath, FileMode.Create))
                            {
                                await attachment.CopyToAsync(stream);
                            }
                        }
                    }
                    // Store attachment paths in the database
                    taskModel.Attachments = attachmentPaths.Count > 0 ? attachmentPaths : null;
                    var createdTask = await _taskService.CreateTask(taskModel);

                    return CreatedAtAction(nameof(GetTask), new { id = createdTask.Id }, createdTask);
                }
                else
                {
                    return BadRequest("Task model is null.");
                }
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //[HttpPut("{id}")]
        //public async Task<IActionResult> UpdateTask(int id, Task task)
        //{
        //    if (id != task.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(task).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!TaskExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteTask(int id)
        //{
        //    var task = await _context.Tasks.FindAsync(id);
        //    if (task == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Tasks.Remove(task);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        //private bool TaskExists(int id)
        //{
        //    return _context.Tasks.Any(e => e.Id == id);
        //}
    }
}
