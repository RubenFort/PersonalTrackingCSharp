using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.DTO;

namespace DAL.DAO
{
    public class TaskDAO : EmployeeContext
    {
        public static List<TASKSTATE> GetTaskStates()
        {
            return db.TASKSTATE.ToList();
        }

        public static void AddTask(TASK task)
        {
            try
            {
                db.TASK.InsertOnSubmit(task);
                db.SubmitChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static List<TaskDetailDTO> GetTasks()
        {
            List<TaskDetailDTO> taskList = new List<TaskDetailDTO>();

            var list = (from t in db.TASK
                        join s in db.TASKSTATE on t.TaskState equals s.ID
                        join e in db.EMPLOYEE on t.EmployeeID equals e.ID
                        join d in db.DEPARTMENT on e.DepartmentID equals d.ID
                        join p in db.POSITION on e.PositionID equals p.ID
                        select new
                        {
                            taskId = t.ID,
                            title = t.TaskTitle,
                            content = t.TaskContent,
                            starDate = t.TaskStartDate,
                            deliveryDate = t.TaskDeliveryDate,
                            taskStateName = s.StateName,
                            taskStateID = t.TaskState,
                            userNo = e.UserNo,
                            name = e.Name,
                            employeeID = t.EmployeeID,
                            surname = e.SurName,
                            positionName = p.PositionName,
                            departmentName = d.DepartmentName,
                            positionID = e.PositionID,
                            departmentID = e.DepartmentID,
                        }).OrderBy(x => x.starDate).ToList();

            foreach (var item in list)
            {
                TaskDetailDTO dto = new TaskDetailDTO();
                dto.taskID = item.taskId ;
                dto.title = item.title;
                dto.content = item.content;
                dto.taskStartDate = item.starDate;
                dto.taskDeliveryDate = item.deliveryDate;
                dto.taskStateName = item.taskStateName;
                dto.taskStateID = item.taskStateID;
                dto.userNo = item.userNo;
                dto.name = item.name;
                dto.employeeID = item.employeeID;
                dto.surname = item.surname;
                dto.positionName = item.positionName;
                dto.departmentName = item.departmentName;
                dto.positionID = item.positionID;
                dto.departmentID = item.departmentID;
                taskList.Add(dto);
            }

            return taskList;
        }

        public static void DeleteTask(int taskID)
        {
            try
            {
                TASK ts = db.TASK.First(x => x.ID == taskID);
                db.TASK.DeleteOnSubmit(ts);
                db.SubmitChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static void UpdateTask(TASK task)
        {
            try
            {
                TASK ts = db.TASK.First(x => x.ID == task.ID);
                ts.TaskTitle = task.TaskTitle;
                ts.TaskContent = task.TaskContent;
                ts.TaskState = task.TaskState;
                ts.EmployeeID = task.EmployeeID;
                db.SubmitChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
