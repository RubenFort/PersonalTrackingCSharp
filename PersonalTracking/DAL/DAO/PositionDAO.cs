using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.DTO;

namespace DAL.DAO
{
    public class PositionDAO : EmployeeContext, IDAO<PositionDTO, POSITION>
    {
        public static void AddPosition(POSITION position)
        {
            try
            {
                db.POSITION.InsertOnSubmit(position);
                db.SubmitChanges();
            }
            catch (Exception)
            {
            }
        }

        public static List<PositionDTO> GetPositions()
        {
            try
            {
                var list = (from p in db.POSITION
                            join d in db.DEPARTMENT on p.DepartmentID equals d.ID
                            select new
                            {
                                positionID = p.ID,
                                positionName = p.PositionName,
                                departmentName = d.DepartmentName,
                                departmentID = p.DepartmentID
                            }).OrderBy(x => x.positionID).ToList();
                List<PositionDTO> positionList = new List<PositionDTO>();
                foreach (var item in list)
                {
                    PositionDTO dto = new PositionDTO();
                    dto.ID = item.positionID;
                    dto.PositionName = item.positionName;
                    dto.departmentNane = item.departmentName;
                    dto.DepartmentID = item.departmentID;
                    positionList.Add(dto);
                }
                return positionList;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static void DeletePosition(int ID)
        {
            /*
               use [PERSONALTRACKING]
                go

                create trigger delete_position on [dbo].[POSITION]
                after delete as
                BEGIN
                declare @id int
                select @id = ID from deleted
                delete from EMPLOYEE Where PositionID = @id
                END
             */

            try
            {
                POSITION position = db.POSITION.First(x => x.ID == ID);
                db.POSITION.DeleteOnSubmit(position);
                db.SubmitChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void UpdatePosition(POSITION position)
        {
            try
            {
                POSITION pst = db.POSITION.First(x => x.ID == position.ID);
                pst.PositionName = position.PositionName;
                pst.DepartmentID = position.DepartmentID;
                db.SubmitChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<PositionDTO> Select()
        {
            try
            {
                var list = (from p in db.POSITION
                            join d in db.DEPARTMENT on p.DepartmentID equals d.ID
                            select new
                            {
                                positionID = p.ID,
                                positionName = p.PositionName,
                                departmentName = d.DepartmentName,
                                departmentID = p.DepartmentID
                            }).OrderBy(x => x.positionID).ToList();
                List<PositionDTO> positionList = new List<PositionDTO>();
                foreach (var item in list)
                {
                    PositionDTO dto = new PositionDTO();
                    dto.ID = item.positionID;
                    dto.PositionName = item.positionName;
                    dto.departmentNane = item.departmentName;
                    dto.DepartmentID = item.departmentID;
                    positionList.Add(dto);
                }
                return positionList;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool Insert(POSITION entity)
        {
            try
            {
                db.POSITION.InsertOnSubmit(entity);
                db.SubmitChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Update(POSITION entity)
        {
            try
            {
                POSITION pst = db.POSITION.First(x => x.ID == entity.ID);
                pst.PositionName = entity.PositionName;
                pst.DepartmentID = entity.DepartmentID;
                db.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                POSITION position = db.POSITION.First(x => x.ID == id);
                db.POSITION.DeleteOnSubmit(position);
                db.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
