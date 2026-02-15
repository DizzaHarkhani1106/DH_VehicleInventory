using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DH_VehicleInventory.Application.DTOs
{
    public class DH_CreateVehicleDto
    {
        public string VehicleCode { get; set; }
        public int LocationId { get; set; }
        public int VehicleType { get; set; }
    }
}
