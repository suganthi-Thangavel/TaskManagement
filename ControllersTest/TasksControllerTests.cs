using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TaskManagementService.Controllers;
using TaskManagementService.DTO;
using TaskManagementService.Model;
using TaskManagementService.Services;

namespace ControllersTest
{
    public class TasksControllerTests
    {
        [Fact]
        public async Task GetTask_ReturnsOkResult_WhenTaskExists()
        {
            // Arrange
            var mockTaskService = new Mock<ITaskService>();
            mockTaskService.Setup(repo => repo.GetAllTasksAsync())
                           .ReturnsAsync(new List<TaskModel> { new TaskModel { Id = 1, Title = "Task 1" } });
            var mockWebHostEnvironment = new Mock<IWebHostEnvironment>();
            var mockMapper = new Mock<IMapper>();
            var controller = new TasksController(mockTaskService.Object, mockWebHostEnvironment.Object, mockMapper.Object);

            // Act
            var result = await controller.GetTask();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var tasks = Assert.IsAssignableFrom<IEnumerable<TaskModel>>(okResult.Value);
            Assert.NotEmpty(tasks);
        }
       
        [Fact]
        public async Task CreateTask_ReturnsCreatedAtAction_WhenTaskCreatedSuccessfully()
        {
            // Arrange

            var expectedTask = new TaskModel { Id = 1, Title = "New Task" };

            var mockTaskService = new Mock<ITaskService>();
            mockTaskService.Setup(repo => repo.CreateTask(It.IsAny<TaskModel>()))
                           .ReturnsAsync(expectedTask);


            var mockWebHostEnvironment = new Mock<IWebHostEnvironment>();
            var mockMapper = new Mock<IMapper>();

            mockMapper.Setup(mapper => mapper.Map<TaskModel>(It.IsAny<TaskRequestDTO>()))
           .Returns((TaskRequestDTO requestDto) =>
           {
               var taskModel = new TaskModel
               {
                   Id = requestDto.Id,
                   Title = requestDto.Title,
                   Description = requestDto.Description,
                   Completed = requestDto.Completed,
                   DueDate = requestDto.DueDate,
                   ProjectId = requestDto.ProjectId,
                   Assignee = requestDto.Assignee,
                   Priority = requestDto.Priority,
                   Type = requestDto.Type,
                   Labels = requestDto.Labels,
                   Components = requestDto.Components,
                   Versions = requestDto.Versions,
                   Attachments = new List<string>(), // Add attachments if available
                   Comments = requestDto.Comments
               };
               return taskModel;
           });

            var controller = new TasksController(mockTaskService.Object, mockWebHostEnvironment.Object, mockMapper.Object);
           
            var requestDto = new TaskRequestDTO
            {
                Id = 1,
                Title = "Sample Task",
                Description = "This is a sample task description.",
                Completed = false,
                DueDate = DateTime.Now.AddDays(7),
                ProjectId = 123,
                Assignee = "John Doe",
                Priority = "High",
                Type = "Bug",
                Labels = "Bug, Issue",
                Components = "Backend",
                Versions = "1.0",
                Attachments = new List<IFormFile>(),
                Comments = "Sample comment for the task."
            };

            // Act
            var result = await controller.CreateTask(requestDto);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var model = Assert.IsType<TaskModel>(createdAtActionResult.Value);
            Assert.Equal(expectedTask.Id, model.Id);
            Assert.Equal(expectedTask.Title, model.Title);
        }
    }
}
