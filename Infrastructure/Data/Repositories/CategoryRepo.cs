using ApplicationCore.Interfaces.Data;
using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Repositories
{
    public class CategoryRepo : ICategoryRepo, IRepository<Category>
    {
        private SeamlessDigitalContext _dbContext;
        public CategoryRepo(ref SeamlessDigitalContext context) 
        {
            _dbContext = context;
        }

        public void Delete(Category entity)
        {
            throw new NotImplementedException();
        }

        public Task<Category> GetById(long id)
        {
            throw new NotImplementedException();
        }

        public Task<Category> GetForUpdate(long id)
        {
            throw new NotImplementedException();
        }

        public long GetLastInsertedId()
        {
            return _dbContext.Categories.OrderByDescending(x => x.Id).First().Id;
        }

        public async void Insert(Category entity)
        {          
            await _dbContext.Set<Category>().AddAsync(entity);
        }

        public IEnumerable<Category> List()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Category> Page(string search, int skip, int take, out long count)
        {
            throw new NotImplementedException();
        }

        public void Update(Category entity)
        {
            throw new NotImplementedException();
        }
    }
}
