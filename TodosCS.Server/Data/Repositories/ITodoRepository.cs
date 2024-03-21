using TodosCS.Server.Models;

namespace TodosCS.Server.Data.Repositories
{
    public interface ITodoRepository
    {
        Task<IEnumerable<Todo>> GetAll();

        Task<Todo?> GetById(Guid id);

        Task Create(Todo todo);

        Task<Todo?> Update(Guid id, Todo todo);

        Task<bool> Delete(Guid id);
    }
}
