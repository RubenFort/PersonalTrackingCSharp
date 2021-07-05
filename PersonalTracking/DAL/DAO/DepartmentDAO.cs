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

        public static void DeleteDepartment(int ID)
        {
            /*
			   use [PERSONALTRACKING]
               go

               create trigger delete_department on [dbo].[DEPARTMENT]
               after delete as
               BEGIN
               declare @id int
               select @id = ID from deleted
			   delete from EMPLOYEE Where ID = @id
			   delete from POSITION Where ID = @id
               END
            */

            try
            {
                DEPARTMENT department = db.DEPARTMENT.First(x => x.ID == ID);
                db.DEPARTMENT.DeleteOnSubmit(department);
                db.SubmitChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void UpdateDepartment(DEPARTMENT department)
        {
            try
            {
                DEPARTMENT dpt = db.DEPARTMENT.First(x => x.ID == department.ID);
                dpt.DepartmentName = department.DepartmentName;
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
