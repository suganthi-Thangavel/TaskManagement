using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TaskManagementService.Controllers;
using TaskManagementService.DTO;
using TaskManagementService.Model;
using TaskManagementService.Services;

namespace ControllersTest
{
    
    public class ProjectsControllerTests
    {
        [Fact]
        public async Task GetProjects_ReturnsAllProjects()
        {
            // Arrange

            var mockProjects = new List<ProjectModel>
            {
                new ProjectModel
                {
                    Id = 1,
                    Name = "Project 1",
                    Description = "Description for Project 1",
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now.AddDays(7)
                },
                new ProjectModel
                {
                    Id = 2,
                    Name = "Project 2",
                    Description = "Description for Project 2",
                    StartDate = DateTime.Now.AddDays(1),
                    EndDate = DateTime.Now.AddDays(8)
                }
            };
            
            var mockMapper = new Mock<IMapper>();
            var mockProjectService = new Mock<IProjectService>();
            mockProjectService.Setup(svc => svc.GetAllProjectsAsync())
                              .ReturnsAsync(mockProjects);

            var controller = new ProjectsController(mockProjectService.Object, mockMapper.Object);

            // Act
            var result = await controller.GetProjects();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var projects = Assert.IsAssignableFrom<IEnumerable<ProjectModel>>(okResult.Value);
            Assert.NotEmpty(projects);
            Assert.Equal(mockProjects, projects);
        }
        [Fact]
        public async Task CreateProject_ReturnsCreatedAtAction_WhenProjectCreatedSuccessfully()
        {
            // Arrange
            var request = new ProjectRequestDTO
            {
                Id = 1,
                Name = "Project 1",
                Description = "Description for Project 1",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(7)
            };
          
            var expectedProject = new ProjectModel {
                Id = 1,
                Name = "Project 1",
                Description = "Description for Project 1",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(7)
            };
            var mockProjectService = new Mock<IProjectService>();
            mockProjectService.Setup(svc => svc.CreateProjectAsync(It.IsAny<ProjectModel>()))
                              .ReturnsAsync(expectedProject);

            var mockMapper = new Mock<IMapper>();

            mockMapper.Setup(mapper => mapper.Map<ProjectModel>(It.IsAny<ProjectRequestDTO>()));

            var controller = new ProjectsController(mockProjectService.Object, mockMapper.Object);

            // Act
            var result = await controller.CreateProject(request);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var model = Assert.IsType<ProjectModel>(createdAtActionResult.Value);
            Assert.Equal(expectedProject.Id, model.Id);
            Assert.Equal(expectedProject.Name, model.Name);
            Assert.Equal(expectedProject.Description, model.Description);
            Assert.Equal(expectedProject.StartDate, model.StartDate);
        }
    }
}
