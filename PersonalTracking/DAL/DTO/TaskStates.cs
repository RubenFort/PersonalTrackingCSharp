using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DTO
{
    public static class TaskStates
    {
        public static int onEmployee = 1;
        public static int delivery = 2;//Usuario no administrador
        public static int approved = 3;//Administrador
    }
}
