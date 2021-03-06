using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoListApi.Entities;

namespace TodoListApi.Repositories
{
    public interface IUserRepository
    {
        Task<List<User>> GetUserList();
    }
}
