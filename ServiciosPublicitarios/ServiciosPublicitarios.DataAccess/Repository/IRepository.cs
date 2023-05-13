using System;
using System.Collections.Generic;
using System.Text;

namespace ServiciosPublicitarios.DataAccess.Repository
{
    public interface IRepository <T, U>
    {
        public IEnumerable<U> List();
        public RequestStatus Insert(T item);
        public RequestStatus Update(T item);
        public U Find(int? id);
        public RequestStatus Delete(T item);
    }
}
