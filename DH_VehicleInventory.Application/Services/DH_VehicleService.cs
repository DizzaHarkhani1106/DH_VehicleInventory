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

        // Update Vehicle Status
        public async Task<DH_VehicleDto> UpdateVehicleStatusAsync(int id, DH_UpdateVehicleStatusDto dto)
        {
            if (!Enum.IsDefined(typeof(VehicleStatus), dto.Status))
                throw new ArgumentException("Invalid vehicle status.");

            var vehicle = await _repository.GetByIdAsync(id);

            if (vehicle == null)
                throw new KeyNotFoundException("Vehicle not found.");

            var newStatus = (VehicleStatus)dto.Status;

            // Call domain behavior methods instead of changing status directly
            switch (newStatus)
            {
                case VehicleStatus.Available:
                    vehicle.MarkAvailable();
                    break;
                case VehicleStatus.Reserved:
                    vehicle.MarkReserved();
                    break;
                case VehicleStatus.Rented:
                    vehicle.MarkRented();
                    break;
                case VehicleStatus.Maintenance:
                    vehicle.MarkServiced();
                    break;
                default:
                    throw new ArgumentException("Unsupported status transition.");
            }

            await _repository.UpdateAsync(vehicle);

            return MapToDto(vehicle);
        }

        // Delete Vehicle
        public async Task DeleteVehicleAsync(int id)
        {
            var vehicle = await _repository.GetByIdAsync(id);

            if (vehicle == null)
                throw new KeyNotFoundException("Vehicle not found.");

            await _repository.DeleteAsync(id);
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
