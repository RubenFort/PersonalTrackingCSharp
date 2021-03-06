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
    public class SalaryBLL
    {
        public static SalaryDTO GetAll()
        {
            SalaryDTO dto = new SalaryDTO();
            dto.employees = EmployeeDAO.GetEmployees();
            dto.departments = DepartmentDAO.GetDepartments();
            dto.positions = PositionDAO.GetPositions();
            dto.months = SalaryDAO.GetMonths();
            dto.salaries = SalaryDAO.GetSalaies();
            return dto;
        }

        public static void AddSalary(SALARY salary, bool control)
        {
            SalaryDAO.AddSalary(salary);
            if (control)
                EmployeeDAO.UpdateEmployee(salary.EmployeeID, salary.Amount);
        }

        public static void UpdateSalary(SALARY salary, bool control)
        {
            SalaryDAO.UpdateSalary(salary);
            if (control)
                EmployeeDAO.UpdateEmployee(salary.EmployeeID, salary.Amount);
        }

        public static void DeleteSalary(int salaryID)
        {
            SalaryDAO.DeleteSalary(salaryID); ;
        }
    }
}
