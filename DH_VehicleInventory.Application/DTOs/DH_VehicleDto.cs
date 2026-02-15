using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DH_VehicleInventory.Application.DTOs
{
    public class DH_VehicleDto
    {
        public int Id { get; set; }
        public string VehicleCode { get; set; }
        public int LocationId { get; set; }
        public string VehicleType { get; set; }
        public string Status { get; set; }
    }
}
