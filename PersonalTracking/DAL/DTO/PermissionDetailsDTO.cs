using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DTO
{
    public class PermissionDetailsDTO
    {
        public int employeeID { get; set; }
        public int userNo { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string departmentName { get; set; }
        public string positionName { get; set; }
        public int departmentID { get; set; }
        public int positionID { get; set; }
        public DateTime? startDate { get; set; }
        public DateTime? endDate { get; set; }
        public int permissionDayAmount { get; set; }
        public string  stateName { get; set; }
        public int  state { get; set; }
        public string explanation { get; set; }

    }
}
