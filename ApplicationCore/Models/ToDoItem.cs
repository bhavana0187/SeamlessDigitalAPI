using ApplicationCore.Interfaces.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Models
{
    public class ToDoItem:EntityBase
    {       
        public string ToDo { get; set; }
        public bool Completed { get; set; }
        public int UserId { get; set; }
        public long? CategoryId { get; set; }      
        public Category? Category { get; set; }
        public int Priority { get; set; } = 3;     
        public DateTime? DueDate { get; set; }
    }
}
