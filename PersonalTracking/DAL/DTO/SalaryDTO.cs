using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DTO
{
    public class SalaryDTO
    {
        public List<SalaryDetailDTO> salaries { get; set; }
        public List<EmployeeDetailDTO> employees { get; set; }
        public List<MONTHS> months { get; set; }
        public List<DEPARTMENT> departments { get; set; }
        public List<PositionDTO> positions { get; set; }
    }
}
