using System;

namespace HouseKeeping.Service.Models
{
    public class TaskServiceModel : IdModel
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Budget { get; set; }

        public string PictureUrl { get; set; }

        public LocationServiceModel LocationServiceModel { get; set; }

        public TaskCategoryServiceModel CategoryServiceModel { get; set; }

        public TaskStatusServiceModel StatusServiceModel { get; set; }

        public DateTime Deadline { get; set; }

        public DateTime CompletionDate { get; set; }
    }
}
