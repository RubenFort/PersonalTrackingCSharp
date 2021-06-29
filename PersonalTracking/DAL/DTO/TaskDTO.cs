using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DTO
{
    public class TaskDTO
    {
        public List<EmployeeDetailDTO> employees { get; set; }
        public List<DEPARTMENT> departments { get; set; }
        public List<PositionDTO> positions { get; set; }
        public List<TASKSTATE> taskStates { get; set; }
    }
}
