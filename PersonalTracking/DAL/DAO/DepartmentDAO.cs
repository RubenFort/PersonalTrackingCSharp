using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DAO
{
    public class DepartmentDAO : EmployeeContext
    {
        /// <summary>
        /// Se agrega la entidad department a la tabla DEPARTMENTE
        /// Para finalizar la insercción db.SubmitChanges();
        /// </summary>
        /// <param name="department"></param>
        public static void AddDepartmentDAO(DEPARTMENT department)
        {
            try
            {
                db.DEPARTMENT.InsertOnSubmit(department);
                db.SubmitChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<DEPARTMENT> GetDepartments()
        {
            return db.DEPARTMENT.ToList();
        }
    }
}
