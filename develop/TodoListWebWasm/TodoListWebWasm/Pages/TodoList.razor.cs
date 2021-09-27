using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using TodoList.Models;
using TodoListWebWasm.Services;

namespace TodoListWebWasm.Pages
{
    public partial class TodoList
    {
        [Inject] private ITaskApiClient TaskApiClient { get; set; }

        private List<TaskDto> Tasks;

        protected override async Task OnInitializedAsync()
        {
            Tasks = await TaskApiClient.GetTaskList();
        }
    }
}
