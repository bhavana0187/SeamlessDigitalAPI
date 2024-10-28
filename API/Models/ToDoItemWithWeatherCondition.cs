namespace API.Models
{
    public class ToDoItemWithWeatherCondition:ApiEntityBase
    {
        public string ToDo { get; set; }
        public bool Completed { get; set; }
        public int UserId { get; set; }
        public Category? Category { get; set; }
        public int Priority { get; set; } = 3;
        public Location? Location { get; set; }
        public DateTime? DueDate { get; set; }
        public double? CurrentTemperature { get; set; }
        public string? CurrentCondition { get; set; }

    }
}
