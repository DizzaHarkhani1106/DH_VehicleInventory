using DH_VehicleInventory.Application.DTOs;
using DH_VehicleInventory.Application.Services;
using DH_VehicleInventory.Application.Validators;
using DH_VehicleInventory.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace DH_VehicleInventory.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DH_VehiclesController : ControllerBase
    {
        private readonly DH_VehicleService _vehicleService;
        private readonly DH_CreateVehicleValidator _validator;

        public DH_VehiclesController(DH_VehicleService vehicleService, DH_CreateVehicleValidator validator)
        {
            _vehicleService = vehicleService;
            _validator = validator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var vehicles = await _vehicleService.GetAllVehiclesAsync();
            return Ok(vehicles);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var vehicle = await _vehicleService.GetVehicleByIdAsync(id);
                return Ok(vehicle);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { error = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] DH_CreateVehicleDto dto)
        {
            var validationErrors = _validator.Validate(dto);
            if (validationErrors.Any())
            {
                return BadRequest(new { errors = validationErrors });
            }

            try
            {
                var vehicle = await _vehicleService.CreateVehicleAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = vehicle.Id }, vehicle);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
            catch (DomainException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPut("{id}/status")]
        public async Task<IActionResult> UpdateStatus(int id, [FromBody] DH_UpdateVehicleStatusDto dto)
        {
            try
            {
                var vehicle = await _vehicleService.UpdateVehicleStatusAsync(id, dto);
                return Ok(vehicle);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { error = ex.Message });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
            catch (DomainException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _vehicleService.DeleteVehicleAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { error = ex.Message });
            }
        }
    }
}
