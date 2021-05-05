using System;

namespace HouseKeeping.Data.Models
{
    public class Task : BaseEntity
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Budget { get; set; }

        public string PictureUrl { get; set; }

        public Guid LocationId { get; set; }
        public Location Location { get; set; }

        public Guid CategoryId { get; set; }
        public TaskCategory Category { get; set; }

        public Guid StatusId { get; set; }
        public TaskStatus Status { get; set; }

        public DateTime Deadline { get; set; }

        public DateTime CompletionDate { get; set; }
    }
}
