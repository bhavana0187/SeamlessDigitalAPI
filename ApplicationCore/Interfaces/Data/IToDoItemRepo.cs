﻿using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces.Data
{
    public interface IToDoItemRepo: IRepository<ToDoItem>
    {
        IEnumerable<ToDoItem> GetToDoItemsSearchByTitleOrPriorityOrDueDate(string search);
    }
 
}
