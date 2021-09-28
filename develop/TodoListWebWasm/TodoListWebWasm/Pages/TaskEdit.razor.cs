using Blazored.Toast.Services;
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
    public partial class TaskEdit
    {
        [Inject] private ITaskApiClient TaskApiClient { get; set; }
        [Inject] private IToastService ToastService { get; set; }

        [Parameter]
        public string TaskId { get; set; }

        private TaskUpdateRequest Task;

        protected override async Task OnInitializedAsync()
        {
            var taskDto = await TaskApiClient.GetTaskDetail(TaskId);
            Task = new TaskUpdateRequest
            {
                Name = taskDto.Name,
                Priority = taskDto.Priority
            };
        }

        private async Task SubmitTask(EditContext context)
        {
            var result = await TaskApiClient.UpdateTask(Guid.Parse(TaskId), Task);
            if (result)
            {
                ToastService.ShowSuccess("Cập nhật thành công !", "Success");
                NavigationManager.NavigateTo("/todolist");
            }
            else {
                ToastService.ShowError("Cập nhật chưa thành công !","Error");
            }
        }
    }
}
