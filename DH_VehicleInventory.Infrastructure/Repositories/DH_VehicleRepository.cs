using DH_VehicleInventory.Domain.VehicleAggregate;
using DH_VehicleInventory.Domain.VehicleAggregate.Entities;
using DH_VehicleInventory.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DH_VehicleInventory.Infrastructure.Repositories
{
    public class VehicleRepository : IVehicleRepository
    {
        private readonly DH_InventoryDbContext _context;

        public VehicleRepository(DH_InventoryDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Vehicle Add(Vehicle vehicle)
        {
            return _context.DH_Vehicles.Add(vehicle).Entity;
        }

        public void Update(Vehicle vehicle)
        {
            _context.DH_Vehicles.Update(vehicle);
        }

        public void Delete(Vehicle vehicle)
        {
            _context.DH_Vehicles.Remove(vehicle);
        }

        public async Task<Vehicle> GetByIdAsync(int id)
        {
            return await _context.DH_Vehicles
                .Include(v => v.Inventories)
                .FirstOrDefaultAsync(v => v.Id == id);
        }

        public async Task<IEnumerable<Vehicle>> GetAllAsync()
        {
            return await _context.DH_Vehicles
                .Include(v => v.Inventories)
                .ToListAsync();
        }
    }
}