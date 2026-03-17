using DH_VehicleInventory.Domain.SeedWork;
using DH_VehicleInventory.Domain.VehicleAggregate.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DH_VehicleInventory.Domain.VehicleAggregate.Entities
{
    public class Inventory : Entity
    {
        public int VehicleId { get; private set; }
        public Vehicle Vehicle { get; private set; }

        public Location Location { get; private set; }
        public VehicleStatus Status { get; private set; }
        public DateTime LastUpdated { get; private set; }

        public Inventory() { }

        public Inventory(
            int vehicleId,
            Location location,
            VehicleStatus status)
        {
            if (vehicleId <= 0)
                throw new ArgumentException("Vehicle id must be positive");

            VehicleId = vehicleId;
            Location = location ?? throw new ArgumentNullException(nameof(location));
            Status = status ?? throw new ArgumentNullException(nameof(status));
            LastUpdated = DateTime.UtcNow;
        }

        public void UpdateStatus(VehicleStatus newStatus)
        {
            Status = newStatus ?? throw new ArgumentNullException(nameof(newStatus));
            LastUpdated = DateTime.UtcNow;
        }

        public void MoveToLocation(Location newLocation)
        {
            Location = newLocation ?? throw new ArgumentNullException(nameof(newLocation));
            LastUpdated = DateTime.UtcNow;
        }
    }
}

