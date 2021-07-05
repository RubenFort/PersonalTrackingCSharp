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
            List<EmployeeDetailDTO> employeeList = new List<EmployeeDetailDTO>();
            var list = (from e in db.EMPLOYEE
                        join d in db.DEPARTMENT on e.DepartmentID equals d.ID
                        join p in db.POSITION on e.PositionID equals p.ID
                        select new
                        {
                            UserNo = e.UserNo,
                            Name = e.Name,
                            Surname = e.SurName,
                            EmployeeID = e.ID,
                            Password = e.Password,
                            DepartmentName = d.DepartmentName,
                            PositionName = p.PositionName,
                            DepartmentID = e.DepartmentID,
                            PositionÏD = e.PositionID,
                            isAdmin = e.isAdmin,
                            Salary = e.Salary,
                            ImagePath = e.ImagePath,
                            BirthDay = e.BirthDay,
                            Adress = e.Adress
                        }).OrderBy(x => x.UserNo).ToList();
            //Ejemplo con Where
            //}).OrderBy(x => x.UserNo).Where(x => x.UserNo == 1).ToList();

            foreach (var item in list)
            {
                EmployeeDetailDTO dto = new EmployeeDetailDTO();
                dto.name = item.Name;
                dto.userNo = item.UserNo;
                dto.surname = item.Surname;
                dto.employeeID = item.EmployeeID;
                dto.password = item.Password;
                dto.departmentID = item.DepartmentID;
                dto.departmentName = item.DepartmentName;
                dto.positionID = item.PositionÏD;
                dto.positionName = item.PositionName;
                dto.isAdmin = item.isAdmin.HasValue;
                dto.salary = item.Salary;
                dto.birthDay = item.BirthDay;
                dto.adress = item.Adress;
                dto.imagePath = item.ImagePath;
                employeeList.Add(dto);
            }
            return employeeList;
        }

        public static void DeleteEmployee(int employeeID)
        {
            try
            {
                EMPLOYEE emp = db.EMPLOYEE.First(x => x.ID == employeeID);
                db.EMPLOYEE.DeleteOnSubmit(emp);
                db.SubmitChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static void UpdateEmployee(POSITION position)
        {
            //Recojo los empleados que corresponden a ese puedto de trabajo(position)
            List <EMPLOYEE> list = db.EMPLOYEE.Where(x => x.PositionID == position.ID).ToList();
            foreach (var item in list)
            {
                item.DepartmentID = position.DepartmentID;
            }
            db.SubmitChanges();
        }

        public static void UpdateEmployee(EMPLOYEE employee)
        {
            try
            {
                EMPLOYEE emp = db.EMPLOYEE.First(x => x.ID == employee.ID);
                emp.UserNo = employee.UserNo;
                emp.Name = employee.Name;
                emp.SurName = employee.SurName;
                emp.Password = employee.Password;
                emp.isAdmin = employee.isAdmin;
                emp.BirthDay = employee.BirthDay;
                emp.Adress = employee.Adress;
                emp.DepartmentID = employee.DepartmentID;
                emp.PositionID = employee.PositionID;
                emp.ImagePath = employee.ImagePath;
                emp.Salary = employee.Salary;
                db.SubmitChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void UpdateEmployee(int employeeID, int amount)
        {
            /*Codigo creación trigger en SqlServer
                USE[PERSONALTRACKING]
                GO

                create trigger delete_employee ON[dbo].[EMPLOYEE]

                    after delete as
                    BEGIN

                    declare @id int
                    select @id = ID from deleted
                    delete from TASK Where EmployeeID = @id

                    delete from SALARY Where EmployeeID = @id

                    delete from PERMISSION Where EmployeeID = @id

                    END
             */
            try
            {
                EMPLOYEE emp = db.EMPLOYEE.First(x => x.ID == employeeID);
                db.EMPLOYEE.DeleteOnSubmit(emp);
                db.SubmitChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<EMPLOYEE> GetEmployees(int v, string text)
        {
            try
            {
                List<EMPLOYEE> list = db.EMPLOYEE.Where(x => x.UserNo == v && x.Password == text).ToList();
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<EMPLOYEE> GetUsers(int v)
        {
            return db.EMPLOYEE.Where(x => x.UserNo == v).ToList();
        }
    }
}
