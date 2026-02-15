using DH_VehicleInventory.Application.DTOs;
using DH_VehicleInventory.Application.Interfaces;
using DH_VehicleInventory.Domain.Entities;
using DH_VehicleInventory.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DH_VehicleInventory.Application.Services
{
    public class DH_VehicleService
    {
        private readonly DH_IVehicleRepository _repository;

        public DH_VehicleService(DH_IVehicleRepository repository)
        {
            _repository = repository;
        }

        // Create Vehicle
        public async Task<DH_VehicleDto> CreateVehicleAsync(DH_CreateVehicleDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.VehicleCode))
                throw new ArgumentException("Vehicle code is required.");

            if (dto.LocationId <= 0)
                throw new ArgumentException("Location ID must be a positive number.");

            if (!Enum.IsDefined(typeof(VehicleType), dto.VehicleType))
                throw new ArgumentException("Invalid vehicle type.");

            var vehicle = new Vehicle(
                dto.VehicleCode,
                dto.LocationId,
                (VehicleType)dto.VehicleType
            );

            await _repository.AddAsync(vehicle);

            return MapToDto(vehicle);
        }

        // Get Vehicle By Id
        public async Task<DH_VehicleDto> GetVehicleByIdAsync(int id)
        {
            var vehicle = await _repository.GetByIdAsync(id);

            if (vehicle == null)
                throw new KeyNotFoundException("Vehicle not found.");

            return MapToDto(vehicle);
        }

        // Get All Vehicles
        public async Task<IEnumerable<DH_VehicleDto>> GetAllVehiclesAsync()
        {
            var vehicles = await _repository.GetAllAsync();

            return vehicles.Select(v => MapToDto(v));
        }

        // Map Vehicle entity to DTO
        private DH_VehicleDto MapToDto(Vehicle vehicle)
        {
            return new DH_VehicleDto
            {
                Id = vehicle.Id,
                VehicleCode = vehicle.VehicleCode,
                LocationId = vehicle.LocationId,
                VehicleType = vehicle.VehicleType.ToString(),
                Status = vehicle.Status.ToString()
            };
        }
    }
}
