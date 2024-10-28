using ApplicationCore.Interfaces.Data;
using ApplicationCore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Infrastructure.Data.Repositories
{
    public class ToDoItemRepo : IToDoItemRepo, IRepository<ToDoItem>
    {
        private SeamlessDigitalContext _dbContext;      

        public ToDoItemRepo(ref SeamlessDigitalContext context) 
        {
            _dbContext = context;
        }
        public void Delete(ToDoItem entity)
        {
            _dbContext.Set<ToDoItem>().Remove(entity);
        }

        public async Task<ToDoItem> GetById(long id)
        {
            try
            {
                var books = _dbContext.ToDoItems
                 .Where(p => IsRecommended(p.ToDo))
                 .ToList();
            }
            catch (Exception ex)
            {
                string re=ex.Message;
            }

            var entity = await _dbContext.Set<ToDoItem>()
               .Include(i => i.Category)              
               .FirstOrDefaultAsync(w => w.Id == id);
            return entity;

        }
        bool IsRecommended(string title)
        {
            return title.StartsWith("A") && title.EndsWith("Z");
        }

        public Task<ToDoItem> GetForUpdate(long id)
        {
            throw new NotImplementedException();
        }

        public long GetLastInsertedId()
        {
            return _dbContext.ToDoItems.OrderByDescending(x => x.Id).First().Id;
        }

        public virtual IEnumerable<ToDoItem> GetToDoItemsSearchByTitleOrPriorityOrDueDate(string search)
        {
            IQueryable<ToDoItem> query=null;
           
            if(long.TryParse(search, out long result))
            {
                query = from t in _dbContext.ToDoItems
                            where t.Priority == result
                        select t;

            }
            else if(DateTime.TryParse(search, out DateTime dt))
            {
                query = from t in _dbContext.ToDoItems
                        where  t.DueDate == dt
                        select t;
            }
            else
            {
                query = from t in _dbContext.ToDoItems
                        where t.Category.Title == search
                        select t;             
            }
               
            return query
                .Include(i => i.Category)
                .AsEnumerable();

        }

        public async void Insert(ToDoItem entity)
        {
            await _dbContext.Set<ToDoItem>().AddAsync(entity);
        }

        public IEnumerable<ToDoItem> List()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ToDoItem> Page(string search, int skip, int take, out long count)
        {
            throw new NotImplementedException();
        }

        public void Update(ToDoItem entity)
        {           
            _dbContext.Entry(entity).State = EntityState.Modified;
          
        }
    }
}
