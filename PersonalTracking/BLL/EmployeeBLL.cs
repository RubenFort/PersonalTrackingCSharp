using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DAL.DAO;
using DAL.DTO;

namespace BLL
{
    public class EmployeeBLL
    {
        public static EmployeeDTO GetAll()
        {
            EmployeeDTO dto = new EmployeeDTO();
            dto.Departements = DepartmentDAO.GetDepartments();
            dto.Positions = PositionDAO.GetPositions();
            return dto;
        }

        public static void AddEmployee(EMPLOYEE employee)
        {
            EmployeeDAO.AddEmployee(employee);
        }
    }
}
