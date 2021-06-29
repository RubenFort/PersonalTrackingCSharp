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
    public class PermissionBLL
    {
        public static void AddPermisssion(PERMISSION permission)
        {
            PermisionDAO.addPermision(permission);
        }

        public static PermissionDTO getAll()
        {
            PermissionDTO dto = new PermissionDTO();
            dto.departments = DepartmentDAO.GetDepartments();
            dto.positions = PositionDAO.GetPositions();
            dto.states = PermisionDAO.GetStates();
            dto.permissions = PermisionDAO.GetPermissions();
            return dto;
        }

        public static void UpdatePermisssion(PERMISSION permission)
        {
            PermisionDAO.UpdatePermission(permission);
        }

        public static void UpdatePermisssion(int permissionID, int approved)
        {
            PermisionDAO.UpdatePermission(permissionID, approved);
        }

        public static void DeletePermission(int permissionID)
        {
            PermisionDAO.DeletePermission(permissionID);
        }
    }
}
