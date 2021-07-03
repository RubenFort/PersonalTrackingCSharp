using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DTO
{
    public class PositionDTO : POSITION 
    {
        public string departmentNane { get; set; }
        public int oldDepartmentID { get; set; }
    }
}
