namespace TodoApi.Models
{
    public class TodoItem
    {
        public int Id { get; set; } // Primary key
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}
