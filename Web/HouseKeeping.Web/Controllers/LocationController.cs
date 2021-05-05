using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HouseKeeping.Service.Interfaces;
using HouseKeeping.Service.Models;
using HouseKeeping.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace HouseKeeping.Web.Controllers
{
    public class LocationController : Controller
    {
        private readonly ILocationService _locationService;

        public LocationController(ILocationService locationService)
        {
            this._locationService = locationService;
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] LocationWebModel locationWebModel)
        {
            LocationServiceModel returnedLocationServiceModel =
                await this._locationService.CreateLocationAsync(LocationController.MapToServiceModel(locationWebModel));

            return RedirectToPage("GetLocationById", returnedLocationServiceModel.Id);
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid locationId)
        {
            LocationServiceModel locationServiceModel = await _locationService.GetByIdAsync(locationId);

            return View(LocationController.MapToWebModel(locationServiceModel));
        }

        [HttpGet]
        public async Task<IActionResult> AllUserLocation(Guid userId)
        {
            List<LocationServiceModel> locationServiceModel = await _locationService.GetAllUserLocationsAsync(userId);

            List<LocationWebModel> locationWebModels = new();
            foreach (var location in locationServiceModel)
                locationWebModels.Add(LocationController.MapToWebModel(location));

            return View(locationWebModels);
        }

        // [Authorize(Roles = Role.Admin)]
        [HttpGet]
        public async Task<IActionResult> AllLocations()
        {
            List<LocationServiceModel> locationServiceModel = await _locationService.GetAllLocationsAsync();

            List<LocationWebModel> locationWebModels = new();
            foreach (var location in locationServiceModel)
                locationWebModels.Add(LocationController.MapToWebModel(location));

            return View(locationWebModels);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteLocation(Guid locationId)
        {
            await this._locationService.DeleteLocation(locationId);

            return RedirectToAction("GetAllUserLocation");
        }

        private static LocationServiceModel MapToServiceModel(LocationWebModel locationWebModel)
        {
            return new LocationServiceModel()
            {
                Id = locationWebModel.Id,
                Name = locationWebModel.Name,
                Address = locationWebModel.Address,
            };
        }

        private static LocationWebModel MapToWebModel(LocationServiceModel locationServiceModel)
        {
            return new LocationWebModel()
            {
                Id = locationServiceModel.Id,
                Name = locationServiceModel.Name,
                Address = locationServiceModel.Address,
            };
        }
    }
}
