using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using TodoList.Models;

namespace TodoListWebWasm.Services
{
    public class TaskApiClient : ITaskApiClient
    {
        private HttpClient _httpClient;

        public TaskApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<TaskDto>> GetTaskList()
        {
            var result = await _httpClient.GetFromJsonAsync<List<TaskDto>>("/api/tasks");
            return result;
        }

        public async Task<TaskDto> GetTaskDetail(string id)
        {
            return await _httpClient.GetFromJsonAsync<TaskDto>($"/api/tasks/{id}");
        }

    }
}
