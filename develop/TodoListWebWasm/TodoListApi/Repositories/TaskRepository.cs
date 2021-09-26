using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoListApi.Data;
using Task = TodoListApi.Entities.Task;

namespace TodoListApi.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly TodoListDbContext _context;

        public TaskRepository(TodoListDbContext context)
        {
            this._context = context;
        }

        public async Task<IEnumerable<Task>> GetTaskList()
        {
            return await _context.Tasks.ToListAsync();
        }

        public async Task<Task> GetById(Guid id)
        {
            var task = await _context.Tasks.FindAsync(id);
            return task;
        }

        public async Task<Task> Create(Entities.Task task)
        {
            await _context.Tasks.AddAsync(task);
            await _context.SaveChangesAsync();
            return task;
        }

        public async Task<Task> Update(Entities.Task task)
        {
            _context.Tasks.Update(task);
            await _context.SaveChangesAsync();
            return task;
        }

        public async Task<Task> Delete(Entities.Task task)
        {
            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();
            return task;
        }

    }
}
