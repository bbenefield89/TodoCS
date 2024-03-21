using Microsoft.EntityFrameworkCore;
using TodosCS.Server.Models;

namespace TodosCS.Server.Data.Repositories
{
    public class DatabaseTodoRepository : ITodoRepository
    {
        private readonly TodoContext _context;

        public DatabaseTodoRepository(TodoContext context)
        {
            _context = context;
        }

        public async Task Create(Todo todo)
        {
            await _context.Todos.AddAsync(todo);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> Delete(Guid id)
        {
            var todo = await _context.Todos.FirstOrDefaultAsync(t => t.Id == id);

            if (todo == null)
            {
                return false;
            }

            _context.Todos.Remove(todo);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Todo>> GetAll()
        {
            return await _context.Todos.ToListAsync();
        }

        public async Task<Todo?> GetById(Guid id)
        {
            return await _context.Todos.FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<Todo?> Update(Guid id, Todo todo)
        {
            var found = await _context.Todos.FirstOrDefaultAsync(t => t.Id == id);

            if (found == null)
            {
                return null;
            }

            found.Title = todo.Title;
            found.IsComplete = todo.IsComplete;

            await _context.SaveChangesAsync();
            return found;
        }
    }
}
