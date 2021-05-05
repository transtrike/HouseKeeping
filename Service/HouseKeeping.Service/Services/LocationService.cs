using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HouseKeeping.Data;
using HouseKeeping.Data.Models;
using HouseKeeping.Service.Interfaces;
using HouseKeeping.Service.Models;
using HouseKeeping.Service.Models.Constants;
using Microsoft.EntityFrameworkCore;

namespace HouseKeeping.Service.Services
{
    public class LocationService : ILocationService
    {
        private readonly HouseKeepingContext _context;

        public LocationService(HouseKeepingContext context)
        {
            this._context = context;
        }

        public async Task<LocationServiceModel> CreateLocationAsync(LocationServiceModel locationServiceModel)
        {
            if (await this._context.Locations.AnyAsync(x => x.Name == locationServiceModel.Name))
                throw new ArgumentException(string.Format(ExceptionConstants.AlreadyExists, locationServiceModel.Name));

            Location location = new()
            {
                Name = locationServiceModel.Name,
                Address = locationServiceModel.Address,
            };

            await this._context.AddAsync(location);
            await this._context.SaveChangesAsync();

            location = await this._context
                .Locations
                .FirstOrDefaultAsync(x => x.Name == locationServiceModel.Name);

            return LocationService.MapToServiceModel(location);
        }

        public async Task<List<LocationServiceModel>> GetAllLocationsAsync()
        {
            List<Location> locations = await this._context.Locations.ToListAsync();

            List<LocationServiceModel> locationServiceModels = new(locations.Count);
            foreach (var location in locations)
                locationServiceModels.Add(LocationService.MapToServiceModel(location));

            return locationServiceModels;
        }

        public async Task<List<LocationServiceModel>> GetAllUserLocationsAsync(Guid userId)
        {
            AppUser issuer = await this._context.Users.FindAsync(userId) ??
                throw new InvalidOperationException(string.Format(ExceptionConstants.DoesNotExist, nameof(AppUser)));

            List<Location> locations = issuer.Locations;

            List<LocationServiceModel> locationServiceModels = new(locations.Count);
            foreach (var location in locations)
                locationServiceModels.Add(LocationService.MapToServiceModel(location));

            return locationServiceModels;
        }

        public async Task<LocationServiceModel> GetByIdAsync(Guid id)
        {
            Location location = await this._context.Locations.FindAsync(id) ??
                throw new InvalidOperationException(string.Format(ExceptionConstants.DoesNotExist, nameof(Location)));

            return LocationService.MapToServiceModel(location);
        }

        public async Task<LocationServiceModel> UpdateLocationAsync(LocationServiceModel locationServiceModel)
        {
            this._context.Locations.Update(LocationService.MapToPureModel(locationServiceModel));
            await this._context.SaveChangesAsync();

            return locationServiceModel;
        }

        public async Task<LocationServiceModel> DeleteLocation(Guid id)
        {
            Location location = await this._context.Locations.FindAsync(id) ??
                throw new InvalidOperationException(string.Format(ExceptionConstants.DoesNotExist, nameof(Location)));

            if (await this._context.Tasks.AnyAsync(x => x.Location == location))
                throw new InvalidOperationException(string.Format(ExceptionConstants.InvalidDelete, nameof(Location)))

            this._context.Locations.Remove(location);
            await this._context.SaveChangesAsync();

            return LocationService.MapToServiceModel(location);
        }

        public static LocationServiceModel MapToServiceModel(Location location)
        {
            return new LocationServiceModel()
            {
                Id = location.Id == Guid.Empty ?
                    Guid.Empty :
                    location.Id,
                Name = location.Name,
                Address = location.Address
            };
        }

        public static Location MapToPureModel(LocationServiceModel locationServiceModel)
        {
            return new Location()
            {
                Id = locationServiceModel.Id,
                Name = locationServiceModel.Name,
                Address = locationServiceModel.Address
            };
        }
    }
}
