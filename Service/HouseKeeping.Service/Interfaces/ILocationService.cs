using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HouseKeeping.Service.Models;

namespace HouseKeeping.Service.Interfaces
{
    public interface ILocationService
    {
        Task<LocationServiceModel> CreateLocationAsync(LocationServiceModel locationServiceModel);

        Task<LocationServiceModel> GetByIdAsync(Guid id);

        Task<List<LocationServiceModel>> GetAllLocationsAsync();

        Task<List<LocationServiceModel>> GetAllUserLocationsAsync(Guid userId);

        Task<LocationServiceModel> UpdateLocationAsync(LocationServiceModel locationServiceModel);

        Task<LocationServiceModel> DeleteLocation(Guid id);
    }
}
