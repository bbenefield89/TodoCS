namespace TodosCS.Server.Models
{
    public class Todo
    {
        public Guid Id { get; private set; }
        public string Title { get; set; }
        public bool IsComplete { get; set; }

        public Todo()
        {
            Id = Guid.NewGuid();
        }
    }
}
