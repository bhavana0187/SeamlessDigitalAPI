using ApplicationCore.Interfaces.Data;
using ApplicationCore.Interfaces.Services;
using Infrastructure.Data;
using Infrastructure.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class UnitOfWork : IUnitOfWork,IDisposable
    {
        private SeamlessDigitalContext _dbContext;

        public UnitOfWork(SeamlessDigitalContext context)
        {
            _dbContext = context;
        }

        private CategoryRepo categoryRepo;
        public ICategoryRepo CategoryRepo
        {
            get
            {
                if (this.categoryRepo == null)
                    this.categoryRepo = new CategoryRepo(ref _dbContext);
                return categoryRepo;
            }
        }

        private ToDoItemRepo toDoItemRepo;
        public IToDoItemRepo ToDoItemRepo
        {
            get
            {
                if (this.toDoItemRepo == null)
                    this.toDoItemRepo = new ToDoItemRepo(ref _dbContext);
                return toDoItemRepo;
            }
        }

        //GENERIC METHODS
        public void Save()
        {
            _dbContext.SaveChanges();
        }
        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    //_dbContext.Dispose();
                }
            }
            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
