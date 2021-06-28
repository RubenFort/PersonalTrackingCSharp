using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DTO
{
    public class EmployeeDetailDTO
    {
        public int employeeID { get; set; }
        public int userNo { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string departmentName { get; set; }
        public string positionName { get; set; }
        public int departmentID { get; set; }
        public int positionID { get; set; }
        public int salary { get; set; }
        public bool isAdmin { get; set; }
        public string imagePath { get; set; }
        public string adress { get; set; }
        public DateTime? birthDay { get; set; }
    }
}
