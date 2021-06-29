using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.DTO;

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

        public static List<PERMISSIONSTATE> GetStates()
        {
            return db.PERMISSIONSTATE.ToList();
        }

        public static List<PermissionDetailDTO> GetPermissions()
        {
            List<PermissionDetailDTO> permissions = new List<PermissionDetailDTO>();

            var list = (from p in db.PERMISSION
                        join s in db.PERMISSIONSTATE on p.PermissionState equals s.ID
                        join e in db.EMPLOYEE on p.EmployeeID equals e.ID
                        select new
                        {
                            userNo = e.UserNo,
                            name = e.Name,
                            surname = e.SurName,
                            stateName = s.StateName,
                            stateID = p.PermissionState,
                            startDate = p.PermissionStartDate,
                            endDate = p.PermissionEndDate,
                            employeeID = p.EmployeeID,
                            permissionID = p.ID,
                            explanationID = p.PermissionExplanation,
                            dayAmount = p.PermissionDay,
                            departmentID = e.DepartmentID,
                            positionID = e.PositionID
                        }).OrderBy(x => x.startDate).ToList();

            foreach (var item in list)
            {
                PermissionDetailDTO dto = new PermissionDetailDTO();

                dto.userNo = item.userNo;
                dto.name = item.name;
                dto.surname = item.surname;
                dto.employeeID = item.employeeID;
                dto.permissionDayAmount = item.dayAmount;
                dto.startDate = item.startDate;
                dto.endDate = item.endDate;
                dto.departmentID = item.departmentID;
                dto. positionID= item.positionID;
                dto.state = item.stateID;
                dto.stateName = item.stateName;
                dto.explanation = item.explanationID;
                permissions.Add(dto);
            }
            return permissions;
        }
    }
}
