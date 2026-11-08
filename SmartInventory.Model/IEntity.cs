using System;
using System.Collections.Generic;
using System.Text;

namespace SmartInventory.Model
{
    internal interface IEntity<T1>
    {
        T1 Id { get; set; }
        T1 CreatedBy { get; set; }
        DateTime CreatedDate { get; set; }
        T1 UpdatedBy { get; set; }
        DateTime UpdatedDate { get; set; }

    }

    public class Entity : IEntity<int>
    {
        public int Id { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
