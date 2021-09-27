using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoList.Models;
using TodoListWebWasm.Services;

namespace TodoListWebWasm.Pages
{
    public partial class TaskDetails
    {
        [Inject] private ITaskApiClient TaskApiClient { get; set; }

        [Parameter]
        public string TaskId { get; set; }

        private TaskDto Task;

        protected override async Task OnInitializedAsync()
        {
            Task = await TaskApiClient.GetTaskDetail(TaskId);
        }


    }
}
