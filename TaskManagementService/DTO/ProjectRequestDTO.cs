﻿using System.Text.Json.Serialization;

namespace TaskManagementService.DTO
{
    public class ProjectRequestDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }       
    }
}
