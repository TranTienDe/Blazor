using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoList.Models;
using Task = TodoListApi.Entities.Task;

namespace TodoListApi.Repositories
{
    public interface ITaskRepository
    {
        Task<IEnumerable<Task>> GetTaskList(TaskListSearch taskListSearch);
        Task<Task> Create(Task task);
        Task<Task> Update(Task task);
        Task<Task> Delete(Task task);
        Task<Task> GetById(Guid id);

    }
}
