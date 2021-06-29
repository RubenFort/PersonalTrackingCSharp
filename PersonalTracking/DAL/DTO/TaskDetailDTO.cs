using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DTO
{
    public class TaskDetailDTO
    {
        public int employeeID { get; set; }
        public int userNo { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string departmentName { get; set; }
        public string positionName { get; set; }
        public int departmentID { get; set; }
        public int positionID { get; set; }
        public int taskID { get; set; }
        public string title { get; set; }
        public string content { get; set; }
        public string taskStateName { get; set; }
        public DateTime taskStartDate { get; set; }
        public DateTime taskDeliveryDate { get; set; }
    }
}
