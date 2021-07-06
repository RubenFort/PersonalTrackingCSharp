using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DAO
{
    interface IDAO<T, K> where T : class where K : class
    {
        List<T> Select();
        bool Insert(K entity);
        bool Update(K entity);
        bool Delete(int id);
    }
}
