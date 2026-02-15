using DH_VehicleInventory.Domain.Enums;
using DH_VehicleInventory.Domain.Exceptions;

namespace DH_VehicleInventory.Domain.Entities
{
    public class Vehicle
    {
        public int Id { get; private set; }

        public string VehicleCode { get; private set; }
        public int LocationId { get; private set; }
        public VehicleType VehicleType { get; private set; }
        public VehicleStatus Status { get; private set; }

        private Vehicle() { }

        public Vehicle(string vehicleCode, int locationId, VehicleType vehicleType)
        {
            if (string.IsNullOrWhiteSpace(vehicleCode))
                throw new DomainException("Vehicle code cannot be empty.");

            if (locationId <= 0)
                throw new DomainException("Location ID must be a positive number.");

            VehicleCode = vehicleCode;
            LocationId = locationId;
            VehicleType = vehicleType;
            Status = VehicleStatus.Available;
        }
    }
}