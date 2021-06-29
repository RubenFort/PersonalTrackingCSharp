using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DAO
{
    public class PermisionDAO : EmployeeContext
    {
        public static void addPermision(PERMISSION permission)
        {
            try
            {
                db.PERMISSION.InsertOnSubmit(permission);
                db.SubmitChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
