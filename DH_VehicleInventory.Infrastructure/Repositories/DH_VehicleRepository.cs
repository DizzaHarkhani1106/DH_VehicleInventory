using DH_VehicleInventory.Application.Interfaces;
using DH_VehicleInventory.Domain.Entities;
using DH_VehicleInventory.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace DH_VehicleInventory.Infrastructure.Repositories
{
    public class DH_VehicleRepository : DH_IVehicleRepository
    {
        private readonly DH_InventoryDbContext _context;

        public DH_VehicleRepository(DH_InventoryDbContext context)
        {
            _context = context;
        }

        public async Task<Vehicle> GetByIdAsync(int id)
        {
            return await _context.DH_Vehicles.FindAsync(id);
        }

        public async Task<IEnumerable<Vehicle>> GetAllAsync()
        {
            return await _context.DH_Vehicles.ToListAsync();
        }

        public async Task AddAsync(Vehicle vehicle)
        {
            await _context.DH_Vehicles.AddAsync(vehicle);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Vehicle vehicle)
        {
            _context.DH_Vehicles.Update(vehicle);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var vehicle = await _context.DH_Vehicles.FindAsync(id);
            if (vehicle != null)
            {
                _context.DH_Vehicles.Remove(vehicle);
                await _context.SaveChangesAsync();
            }
        }
    }
}