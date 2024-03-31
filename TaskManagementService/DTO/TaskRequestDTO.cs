using System.Text.Json.Serialization;

namespace TaskManagementService.DTO
{
    public class TaskRequestDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool Completed { get; set; }
        public DateTime DueDate { get; set; }

        // Foreign key property for the associated project
        public int ProjectId { get; set; }       
        public string Assignee { get; set; }
        public string Priority { get; set; }
        public string Type { get; set; }
        public string Labels { get; set; }
        public string Components { get; set; }
        public string Versions { get; set; }
        public List<IFormFile>? Attachments { get; set; }
        public string Comments { get; set; }
    }
}
