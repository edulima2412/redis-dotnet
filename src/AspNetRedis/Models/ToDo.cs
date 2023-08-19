namespace AspNetRedis.Models
{
    public class ToDo
    {
        public ToDo(string title, string description)
        {
            Id = Guid.NewGuid();
            Title = title;
            Description = description;
            Done = false;
            CreatedAt = DateTime.Now;
        }

        public Guid Id { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public bool Done { get; private set; }
        public DateTime CreatedAt { get; set; }
    }
}
