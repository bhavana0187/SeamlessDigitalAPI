namespace API.Models
{
    public class ToDoItem:ApiEntityBase    {       
           
            public string ToDo { get; set; }
            public bool Completed { get; set; }
            public int UserId { get; set; }
            public Category? Category { get; set; }
            public int Priority { get; set; } = 3;
            public Location? Location { get; set; }
            public DateTime? DueDate { get; set; }
       
    }

    public class Location
    {
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
    }    
}
