using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.DTO;
using DAL.DAO;
using DAL;

namespace BLL
{
    public class TaskBLL
    {
        public static TaskDTO GetAll()
        {
            TaskDTO taskDTO = new TaskDTO();
            taskDTO.employees = EmployeeDAO.GetEmployees();
            taskDTO.departments = DepartmentDAO.GetDepartments();
            taskDTO.positions = PositionDAO.GetPositions();
            taskDTO.taskStates = TaskDAO.GetTaskStates();

            return taskDTO;
        }

        public static void AddTask(TASK task)
        {
            TaskDAO.AddTask(task);
        }
    }
}
