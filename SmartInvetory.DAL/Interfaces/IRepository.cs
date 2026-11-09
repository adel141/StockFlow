using System;
using System.Collections.Generic;
using System.Text;

namespace SmartInvetory.DAL.Interfaces
{
    public interface IRepository<CLASS,ID> 
    {
        CLASS GetByID(ID id);
        List<CLASS> GetAll();
        bool Add(CLASS entity);
        bool Update(CLASS entity);
        bool Delete(ID id);
    }
}
