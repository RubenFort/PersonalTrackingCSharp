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
    }
}
