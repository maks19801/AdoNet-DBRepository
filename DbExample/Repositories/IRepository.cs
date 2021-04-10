using System;
using System.Collections.Generic;
using System.Text;

namespace DbExample
{
    public interface IRepository<T> where T : BaseEntity
    {
        IEnumerable<T> Get();
        Book Get(int id);
        void Add(T entity);
        void Update(T entity);
        void Delete(int id);
    }
}
