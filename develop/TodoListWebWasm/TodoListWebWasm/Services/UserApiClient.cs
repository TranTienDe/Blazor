using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using TodoList.Models;

namespace TodoListWebWasm.Services
{
    public class UserApiClient : IUserApiClient
    {
        private readonly HttpClient _httpClient;

        public UserApiClient(HttpClient httpClient)
        {
            this._httpClient = httpClient;
        }

        public async Task<List<AssigneeDto>> GetUserList()
        {
            return await _httpClient.GetFromJsonAsync<List<AssigneeDto>>("/api/users");
        }
    }
}
