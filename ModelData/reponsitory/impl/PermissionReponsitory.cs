
using ModelClassLibrary.connection;
using REPORT.reponsitory;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;


namespace ModelClassLibrary.reponsitory.imlp
{
    public class PermissionReponsitory<T> : IReponsitory<T> where T : class
    {
        protected readonly PermissionContext m_context;
        private DbSet<T> m_table = null;
        public PermissionReponsitory(PermissionContext context)
        {
            m_context = context;
            m_table = m_context.Set<T>();
        }
        public void delete(object id)
        {
            T obj = m_table.Find(id);
            m_context.Entry(obj).State = EntityState.Deleted;
            save();
        }
        public void insert(T obj)
        {
            m_context.Entry(obj).State = EntityState.Added;
            save();
        }
        public void save()
        {
            m_context.SaveChanges();
        }
        public IEnumerable<T> getAll()
        {
            return m_table.ToList();
        }
        public T getById(object id)
        {
            return m_table.Find(id);
        }
        public void update(T obj)
        {
            m_context.Entry(obj).State = EntityState.Modified;
            save();
        }
    }
}
