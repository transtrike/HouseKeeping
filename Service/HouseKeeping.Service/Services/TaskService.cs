using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using HouseKeeping.Data;
using HouseKeeping.Data.Models;
using HouseKeeping.Service.Interfaces;
using HouseKeeping.Service.Models;
using HouseKeeping.Service.Models.Constants;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Task = HouseKeeping.Data.Models.Task;
using TaskStatus = HouseKeeping.Data.Models.TaskStatus;

namespace HouseKeeping.Service.Services
{
    public class TaskService : ITaskService
    {
        private readonly HouseKeepingContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public TaskService(HouseKeepingContext context, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            this._context = context;
            this._userManager = userManager;
            this._signInManager = signInManager;
        }

        public async Task<TaskServiceModel> CreateTaskAsync(TaskServiceModel taskServiceModel)
        {
            Task task = MapToPureModel(taskServiceModel);

            await this._context.Tasks.AddAsync(task);

            task = await this._context.Tasks.FindAsync(task);
            return TaskService.MapToServiceModel(task);
        }

        public async Task<List<TaskServiceModel>> GetAllAssignedTasks(Guid userId)
        {
            AppUser user = await this._context.Users.FirstOrDefaultAsync(x => x.Id == userId.ToString()) ??
                throw new ArgumentException(string.Format(ExceptionConstants.DoesNotExist, nameof(AppUser)));

            List<TaskServiceModel> taskServiceModels = new();
            foreach (var taskService in user.AssignedTasks)
                taskServiceModels.Add(TaskService.MapToServiceModel(taskService));

            return taskServiceModels;
        }

        public async Task<List<TaskServiceModel>> GetAllCreatedTasks(Guid userId)
        {
            AppUser user = await this._context.Users.FirstOrDefaultAsync(x => x.Id == userId.ToString()) ??
                throw new ArgumentException(string.Format(ExceptionConstants.DoesNotExist, nameof(AppUser)));

            List<TaskServiceModel> taskServiceModels = new();
            foreach (var taskService in user.CreatedTasks)
                taskServiceModels.Add(TaskService.MapToServiceModel(taskService));

            return taskServiceModels;
        }

        public async Task<TaskServiceModel> GetTaskByIdAsync(Guid taskId)
        {
            Task task = await this._context.Tasks.FindAsync(taskId) ??
                throw new ArgumentException(string.Format(ExceptionConstants.DoesNotExist, nameof(Task)));

            return TaskService.MapToServiceModel(task);
        }

        public async Task<TaskServiceModel> UpdateTaskAsync(TaskServiceModel taskServiceModel)
        {
            AppUser user = await this._userManager.GetUserAsync(this._signInManager.Context.User);
            Task task = await this._context.Tasks.FindAsync(taskServiceModel.Id) ??
                throw new ArgumentException(string.Format(ExceptionConstants.DoesNotExist, nameof(Task)));
            bool isAdmin = await this._userManager.IsInRoleAsync(user, Role.Admin);
            bool isHouseKeeper = await this._userManager.IsInRoleAsync(user, Role.HouseKeeper);
            bool isClient = await this._userManager.IsInRoleAsync(user, Role.User);

            if (isClient
                && task.Status.Name == TaskStatus.Awaiting //Is currently awaiting
                && taskServiceModel.StatusServiceModel.Name == TaskStatus.Canceled //Wants to cancel it
                )
            {
                TaskStatus canceled = await this._context
                    .TaskStatuses
                    .FirstOrDefaultAsync(x => x.Name == TaskStatus.Canceled);

                task.Status = canceled;
                task.StatusId = canceled.Id;
            }

            if (isClient
                && task.Status.Name == TaskStatus.ForReview //Is currently for review
                && taskServiceModel.StatusServiceModel.Name == TaskStatus.Completed //Wants to complete it
                )
            {
                //TODO: Upload picture to cloud and put it's url in the property
                // task.PictureUrl = uploadedImage.Url

                TaskStatus completed = await this._context
                    .TaskStatuses
                    .FirstOrDefaultAsync(x => x.Name == TaskStatus.Canceled);

                task.Status = completed;
                task.StatusId = completed.Id;
            }

            if (isHouseKeeper
                && task.Status.Name == TaskStatus.ForReview //Is currently for review
                && taskServiceModel.StatusServiceModel.Name == TaskStatus.Completed //Wants to complete it
                && user.AssignedTasks.Any(x => x.Name == taskServiceModel.Name) //Assigned to him
                )
            {
                //TODO: Upload picture to cloud and put it's url in the property
                // task.PictureUrl = uploadedImage.Url

                TaskStatus completed = await this._context
                    .TaskStatuses
                    .FirstOrDefaultAsync(x => x.Name == TaskStatus.Canceled);

                task.Status = completed;
                task.StatusId = completed.Id;
            }

            if (!isAdmin && task.Status.Name != TaskStatus.Awaiting) //Can't edit it, if the status isn't Awaiting
            {
                throw new InvalidOperationException(string.Format(ExceptionConstants.InvalidEdit, nameof(Task)));
            }

            this._context.Tasks.Update(task);
            await this._context.SaveChangesAsync();

            task = await this._context.Tasks.FindAsync(taskServiceModel.Id);

            return taskServiceModel;
        }

        public async Task<TaskServiceModel> DeleteTaskAsync(Guid taskId)
        {
            Task task = await this._context.Tasks.FindAsync(taskId) ??
                throw new ArgumentException(string.Format(ExceptionConstants.DoesNotExist, nameof(Task)));

            this._context.Tasks.Remove(task);
            await this._context.SaveChangesAsync();

            return TaskService.MapToServiceModel(task);
        }

        public static TaskServiceModel MapToServiceModel(Task task)
        {
            return new TaskServiceModel()
            {
                Id = task.Id == Guid.Empty ?
                    Guid.Empty : task.Id,
                Name = task.Name,
                Budget = task.Budget,
                Description = task.Description,
                Deadline = task.Deadline,
                CompletionDate = task.CompletionDate,
                PictureUrl = string.IsNullOrEmpty(task.PictureUrl) ?
                    string.Empty : task.PictureUrl,
                LocationServiceModel = LocationService.MapToServiceModel(task.Location),
                CategoryServiceModel = TaskCategoryService.MapToServiceModel(task.Category),
                StatusServiceModel = TaskStatusService.MapToServiceModel(task.Status),
            };
        }

        public static Task MapToPureModel(TaskServiceModel taskServiceModel)
        {
            return new Task()
            {
                Id = taskServiceModel.Id == Guid.Empty ?
                    Guid.Empty : taskServiceModel.Id,
                Name = taskServiceModel.Name,
                Budget = taskServiceModel.Budget,
                Description = taskServiceModel.Description,
                Deadline = taskServiceModel.Deadline,
                CompletionDate = taskServiceModel.CompletionDate,
                PictureUrl = string.IsNullOrEmpty(taskServiceModel.PictureUrl) ?
                    string.Empty : taskServiceModel.PictureUrl,
                Location = LocationService.MapToPureModel(taskServiceModel.LocationServiceModel),
                Category = TaskCategoryService.MapToPureModel(taskServiceModel.CategoryServiceModel),
                Status = TaskStatusService.MapToPureModel(taskServiceModel.StatusServiceModel),
            };
        }
    }
}
