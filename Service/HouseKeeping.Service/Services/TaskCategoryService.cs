using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HouseKeeping.Data;
using HouseKeeping.Data.Models;
using HouseKeeping.Service.Interfaces;
using HouseKeeping.Service.Models;

namespace HouseKeeping.Service.Services
{
    public class TaskCategoryService : ITaskCategoryService
    {
        private readonly HouseKeepingContext _context;

        public TaskCategoryService(HouseKeepingContext context)
        {
            this._context = context;
        }

        public Task<TaskCategoryServiceModel> CreateTaskCategoryAsync(TaskCategoryServiceModel taskCategoryServiceModel)
        {
            throw new NotImplementedException();
        }

        public async Task<List<TaskCategoryServiceModel>> GetAllTaskCategoriesAsync()
        {
            List<TaskCategory> taskCategories = this._context.TaskCategories.ToList();

            List<TaskCategoryServiceModel> taskCategoryServiceModels = new();
            foreach (var taskCategory in taskCategories)
                taskCategoryServiceModels.Add(TaskCategoryService.MapToServiceModel(taskCategory));

            return taskCategoryServiceModels;
        }

        public Task<List<TaskCategoryServiceModel>> GetAllUserTaskCategoriesAsync(Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task<TaskCategoryServiceModel> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<TaskCategoryServiceModel> UpdateTaskCategoryAsync(TaskCategoryServiceModel taskCategoryServiceModel)
        {
            throw new NotImplementedException();
        }

        public Task<TaskCategoryServiceModel> DeleteTaskCategory(Guid id)
        {
            throw new NotImplementedException();
        }

        public static TaskCategoryServiceModel MapToServiceModel(TaskCategory taskCategory)
        {
            return new TaskCategoryServiceModel()
            {
                Id = taskCategory.Id,
                Name = taskCategory.Name
            };
        }

        public static TaskCategory MapToPureModel(TaskCategoryServiceModel taskCategoryServiceModel)
        {
            return new TaskCategory()
            {
                Id = taskCategoryServiceModel.Id,
                Name = taskCategoryServiceModel.Name
            };
        }
    }
}
