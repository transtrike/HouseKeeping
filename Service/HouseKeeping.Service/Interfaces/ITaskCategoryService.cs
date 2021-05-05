using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HouseKeeping.Service.Models;

namespace HouseKeeping.Service.Interfaces
{
    public interface ITaskCategoryService
    {
        Task<TaskCategoryServiceModel> CreateTaskCategoryAsync(TaskCategoryServiceModel taskCategoryServiceModel);

        Task<TaskCategoryServiceModel> GetByIdAsync(Guid id);

        Task<List<TaskCategoryServiceModel>> GetAllTaskCategoriesAsync();

        Task<TaskCategoryServiceModel> UpdateTaskCategoryAsync(TaskCategoryServiceModel taskCategoryServiceModel);

        Task<TaskCategoryServiceModel> DeleteTaskCategory(Guid id);
    }
}
