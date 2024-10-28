using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces.Data
{
    /// <summary>
    /// Each entity has the default set of methods which can be added to (for complex queries) in the repository classes.
    /// </summary>
    public interface IRepository<T> where T : EntityBase
    {
        IEnumerable<T> Page(string search, int skip, int take, out long count);
        Task<T> GetById(long id);
        Task<T> GetForUpdate(long id);
        long GetLastInsertedId();
        IEnumerable<T> List();
        void Insert(T entity);
        void Update(T entity);
        void Delete(T entity);       
    }
    /// <summary>
    /// By adding some ubiquitous properties here we can set or use their values using the interface.
    /// It also enforces the use of certain fields in each entity.
    /// </summary>
    public abstract class EntityBase
    {
        public long Id { get; protected set; }       
       
    }
}
