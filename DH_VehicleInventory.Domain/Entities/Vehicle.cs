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

        public void MarkAvailable()
        {
            if (Status == VehicleStatus.Available)
                throw new DomainException("Vehicle is already available.");

            if (Status == VehicleStatus.Reserved)
                throw new DomainException("A reserved vehicle cannot be marked as available without explicit release. Use MarkAvailable only after the reservation is released.");

            Status = VehicleStatus.Available;
        }

        public void MarkRented()
        {
            if (Status == VehicleStatus.Rented)
                throw new DomainException("Vehicle is already rented.");

            if (Status == VehicleStatus.Reserved)
                throw new DomainException("Vehicle is currently reserved and cannot be rented.");

            if (Status == VehicleStatus.Maintenance)
                throw new DomainException("Vehicle is under service and cannot be rented.");

            if (Status != VehicleStatus.Available)
                throw new DomainException($"Vehicle cannot be rented from current status: {Status}.");

            Status = VehicleStatus.Rented;
        }

        public void MarkReserved()
        {
            if (Status == VehicleStatus.Reserved)
                throw new DomainException("Vehicle is already reserved.");

            if (Status == VehicleStatus.Rented)
                throw new DomainException("Vehicle is currently rented and cannot be reserved.");

            if (Status == VehicleStatus.Maintenance)
                throw new DomainException("Vehicle is under service and cannot be reserved.");

            if (Status != VehicleStatus.Available)
                throw new DomainException($"Vehicle cannot be reserved from current status: {Status}.");

            Status = VehicleStatus.Reserved;
        }

        public void MarkServiced()
        {
            if (Status == VehicleStatus.Maintenance)
                throw new DomainException("Vehicle is already under service.");

            if (Status == VehicleStatus.Rented)
                throw new DomainException("Vehicle is currently rented and cannot be sent for service.");

            if (Status == VehicleStatus.Reserved)
                throw new DomainException("Vehicle is currently reserved and cannot be sent for service.");

            if (Status != VehicleStatus.Available)
                throw new DomainException($"Vehicle cannot be sent for service from current status: {Status}.");

            Status = VehicleStatus.Maintenance;
        }

        public void ReleaseReservation()
        {
            if (Status != VehicleStatus.Reserved)
                throw new DomainException("Only reserved vehicles can have their reservation released.");

            Status = VehicleStatus.Available;
        }

        public void UpdateLocation(int newLocationId)
        {
            if (newLocationId <= 0)
                throw new DomainException("Location ID must be a positive number.");

            LocationId = newLocationId;
        }
    }
}