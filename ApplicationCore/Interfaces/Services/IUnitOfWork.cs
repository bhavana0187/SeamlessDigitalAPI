using ApplicationCore.Interfaces.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces.Services
{
    public interface IUnitOfWork
    {
        void Save();
        void Dispose();
        ICategoryRepo CategoryRepo { get; }
        IToDoItemRepo ToDoItemRepo { get; }
    }
}
