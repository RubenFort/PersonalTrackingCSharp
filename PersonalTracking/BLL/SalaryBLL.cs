using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.DTO;
using DAL.DAO;

namespace BLL
{
    public class SalaryBLL
    {
        public static SalaryDTO GetAll()
        {
            SalaryDTO dto = new SalaryDTO();
            dto.employees = EmployeeDAO.GetEmployees();
            dto.departments = DepartmentDAO.GetDepartments();
            dto.positions = PositionDAO.GetPositions();
            dto.months = SalaryDAO.GetMonths();
            return dto;
        }
    }
}
