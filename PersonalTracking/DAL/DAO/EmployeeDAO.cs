using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.DTO;

namespace DAL.DAO
{
    public class EmployeeDAO : EmployeeContext
    {
        public static void AddEmployee(EMPLOYEE employee)
        {
            try
            {
                db.EMPLOYEE.InsertOnSubmit(employee);
                db.SubmitChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static List<EmployeeDetailDTO> GetEmployees()
        {

        }

        public static List<EMPLOYEE> GetUsers(int v)
        {
            return db.EMPLOYEE.Where(x => x.UserNo == v).ToList();
        }
    }
}
