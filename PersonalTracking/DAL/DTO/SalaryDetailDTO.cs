using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DTO
{
    public class SalaryDetailDTO
    {
        public int employeeID { get; set; }
        public int userNo { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string departmentName { get; set; }
        public string positionName { get; set; }
        public int departmentID { get; set; }
        public int positionID { get; set; }
        public string monthName { get; set; }
        public int salaryYear { get; set; }
        public int monthID { get; set; }
        public int salaryAmount { get; set; }
        public int salaryID { get; set; }
        public int oldSalary { get; set; }
    }
}
