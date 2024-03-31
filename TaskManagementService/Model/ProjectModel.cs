using System.Text.Json.Serialization;

namespace TaskManagementService.Model
{
    public class ProjectModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        [JsonIgnore]
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        [JsonIgnore]
        public DateTime? UpdatedAt { get; set; }

        [JsonIgnore]
        public ICollection<TaskModel> Tasks { get; set; }
    }
}
