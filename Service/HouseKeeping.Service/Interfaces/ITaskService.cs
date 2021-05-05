using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HouseKeeping.Service.Models;

namespace HouseKeeping.Service.Interfaces
{
    public interface ITaskService
    {
        Task<TaskServiceModel> CreateTaskAsync(TaskServiceModel taskServiceModel);

        Task<TaskServiceModel> GetTaskByIdAsync(Guid taskId);

        //HouseKeeper Only
        Task<List<TaskServiceModel>> GetAllAssignedTasks(Guid userId);

        Task<List<TaskServiceModel>> GetAllCreatedTasks(Guid userId);

        Task<TaskServiceModel> UpdateTaskAsync(TaskServiceModel taskServiceModel);

        Task<TaskServiceModel> DeleteTaskAsync(Guid taskId);
    }
}
