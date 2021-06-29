using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.DTO;

namespace DAL.DAO
{
    public class SalaryDAO : EmployeeContext
    {
        public static List<MONTHS> GetMonths()
        {
            return db.MONTHS.ToList();
        }

        public static void AddSalary(SALARY salary)
        {
            try
            {
                db.SALARY.InsertOnSubmit(salary);
                db.SubmitChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static List<SalaryDetailDTO> GetSalaies()
        {
            List<SalaryDetailDTO> salaryList = new List<SalaryDetailDTO>();
            var list = (from s in db.SALARY
                        join e in db.EMPLOYEE on s.EmployeeID equals e.ID
                        join m in db.MONTHS on s.MonthID equals m.ID
                        select new
                        {
                            userNo = e.UserNo,
                            name = e.Name,
                            surname = e.SurName,
                            employeeID = s.EmployeeID,
                            amount = s.Amount,
                            year = s.Year,
                            monthName = m.MonthName,
                            monthID = s.MonthID,
                            salaryID = s.ID,
                            departmentID = e.DepartmentID,
                            positionID = e.PositionID
                        }).OrderBy(x => x.year).ToList();

            foreach (var item in list)
            {
                SalaryDetailDTO dto = new SalaryDetailDTO();
                dto.userNo = item.userNo;
                dto.name = item.name;
                dto.surname = item.surname;
                dto.employeeID = item.employeeID;
                dto.salaryAmount = item.amount;
                dto.salaryYear = item.year;
                dto.monthName = item.monthName;
                dto.monthID = item.monthID;
                dto.salaryID = item.salaryID;
                dto.departmentID = item.departmentID;
                dto.positionID = item.positionID;
                dto.oldSalary = item.amount;
                salaryList.Add(dto);
            }

            return salaryList;
        }
    }
}
