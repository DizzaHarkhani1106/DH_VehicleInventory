using DH_VehicleInventory.Application.DTOs;
using System.Collections.Generic;

namespace DH_VehicleInventory.Application.Validators
{
    public class DH_CreateVehicleValidator
    {
        public List<string> Validate(DH_CreateVehicleDto dto)
        {
            var errors = new List<string>();

            if (string.IsNullOrWhiteSpace(dto.VehicleCode))
                errors.Add("Vehicle code is required.");

            if (dto.VehicleCode != null && dto.VehicleCode.Length > 50)
                errors.Add("Vehicle code cannot exceed 50 characters.");

            if (dto.LocationId <= 0)
                errors.Add("Location ID must be a positive number.");

            if (dto.VehicleType < 1 || dto.VehicleType > 4)
                errors.Add("Vehicle type must be between 1 (Sedan) and 4 (Van).");

            return errors;
        }
    }
}
