using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TaskManagementService.Model
{
    public class TaskModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool Completed { get; set; }
        public DateTime DueDate { get; set; }

        // Foreign key property for the associated project
        public int ProjectId { get; set; }

        [JsonIgnore]
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        [JsonIgnore]
        public DateTime? UpdatedAt { get; set; }
        public string Assignee { get; set; }
        public string Priority { get; set; }
        public string Type { get; set; }
        public string Labels { get; set; }
        public string Components { get; set; }
        public string Versions { get; set; }
        public List<string>? Attachments { get; set; }
        public string Comments { get; set; }

        [JsonIgnore]
        public ProjectModel Project { get; set; }
    }
}
