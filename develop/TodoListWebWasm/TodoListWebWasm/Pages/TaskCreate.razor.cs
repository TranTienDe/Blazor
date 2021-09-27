using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoList.Models;
using TodoListWebWasm.Services;

namespace TodoListWebWasm.Pages
{
    public partial class TaskCreate
    {
        [Inject] private ITaskApiClient TaskApiClient { get; set; }

        private TaskCreateRequest Task = new TaskCreateRequest();

        private async Task SubmitTask(EditContext context)
        {
            var result = await TaskApiClient.CreateTask(Task);
            if (result)
            {
                NavigationManager.NavigateTo("/todolist");
            }
        }
    }
}
