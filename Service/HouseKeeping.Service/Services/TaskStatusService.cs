using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HouseKeeping.Data;
using HouseKeeping.Service.Models;
using Microsoft.EntityFrameworkCore;
using TaskStatus = HouseKeeping.Data.Models.TaskStatus;

namespace HouseKeeping.Service.Services
{
    public class TaskStatusService : ITaskStatusService
    {
        private readonly HouseKeepingContext _context;

        public TaskStatusService(HouseKeepingContext context)
        {
            this._context = context;
        }

        public Task<TaskStatusServiceModel> CreateTaskStatusAsync(TaskStatusServiceModel taskStatusServiceModel)
        {
            throw new NotImplementedException();
        }

        public async Task<List<TaskStatusServiceModel>> GetAllTaskCategoriesAsync()
        {
            List<TaskStatus> tasksStatuses = await this._context.TaskStatuses.ToListAsync();

            List<TaskStatusServiceModel> taskStatusServiceModels = new();
            foreach (var taskStatus in tasksStatuses)
                taskStatusServiceModels.Add(TaskStatusService.MapToServiceModel(taskStatus));

            return taskStatusServiceModels;
        }

        public Task<TaskStatusServiceModel> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<TaskStatusServiceModel> UpdateTaskStatusAsync(TaskStatusServiceModel taskStatusServiceModel)
        {
            throw new NotImplementedException();
        }

        public Task<TaskStatusServiceModel> DeleteTaskStatus(Guid id)
        {
            throw new NotImplementedException();
        }

        private static TaskStatusServiceModel MapToServiceModel(TaskStatus taskStatus)
        {
            return new TaskStatusServiceModel()
            {
                Id = taskStatus.Id,
                Name = taskStatus.Name
            };
        }
    }
}
