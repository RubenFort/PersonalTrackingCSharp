using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DTO
{
    public class PermissionDTO
    {
        public List<DEPARTMENT> departments { get; set; }
        public List<PositionDTO> positions { get; set; }
        public List<PERMISSIONSTATE> states { get; set; }
        public List<PermissionDetailsDTO> permissions { get; set; }

    }
}
