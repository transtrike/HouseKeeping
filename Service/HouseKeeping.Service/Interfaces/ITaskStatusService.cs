using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HouseKeeping.Service.Models;

namespace HouseKeeping
{
    public interface ITaskStatusService
    {
        Task<TaskStatusServiceModel> CreateTaskStatusAsync(TaskStatusServiceModel taskStatusServiceModel);

        Task<TaskStatusServiceModel> GetByIdAsync(Guid id);

        Task<List<TaskStatusServiceModel>> GetAllTaskCategoriesAsync();

        Task<TaskStatusServiceModel> UpdateTaskStatusAsync(TaskStatusServiceModel taskStatusServiceModel);

        Task<TaskStatusServiceModel> DeleteTaskStatus(Guid id);
    }
}
