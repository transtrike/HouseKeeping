using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace HouseKeeping.Data.Models
{
    public class TaskStatus : BaseEntity
    {
        public const string Awaiting = "Чакаща";
        public const string Assigned = "Назначена на домашен помощник";
        public const string ForReview = "За преглед";
        public const string Completed = "Изпълнена";
        public const string Canceled = "Отказана";

        public string Name { get; set; }
    }
}
