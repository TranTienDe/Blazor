using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using TodoList.Models;
using TodoList.Models.Enums;
using TodoList.Models.SeedWork;
using TodoListWebWasm.Components;
using TodoListWebWasm.Pages.Components;
using TodoListWebWasm.Services;

namespace TodoListWebWasm.Pages
{
    public partial class TaskList
    {
        [Inject] private ITaskApiClient TaskApiClient { get; set; }

        [Inject] private IToastService ToastService { get; set; }

        private List<TaskDto> Tasks;
        private TaskListSearch TaskListSearch = new TaskListSearch();
        private TaskDto TaskItem { get; set; }

        private Confirmation DeleteConfirmation { get; set; } // Đối tượng tham chiếu tối View.

        protected AssignTask AssignTaskDialog { set; get; }

        public MetaData MetaData { get; set; } = new MetaData();

        protected override async Task OnInitializedAsync()
        {
            await GetTasks();
        }

        private async Task SearchForm(TaskListSearch taskListSearch)
        {
            /*ToastService.ShowInfo("Thông là thông báo !","Info");
            ToastService.ShowSuccess("Thông là thông báo !", "Info");
            ToastService.ShowToast(ToastLevel.Info, "Thông là thông báo !", "Info");
            ToastService.ShowWarning("Thông là thông báo !", "Info");
            ToastService.ShowError("Thông là thông báo !", "Info");*/

            TaskListSearch = taskListSearch;
            await GetTasks();
        }

        private async Task OnDeleteTask(Guid id)
        {
            TaskItem = Tasks.FirstOrDefault(x => x.Id == id);
            DeleteConfirmation.Title = "Xóa dữ liệu";
            DeleteConfirmation.Content = $"Bạn chắc chắn muốn xóa {TaskItem.Name}";
            DeleteConfirmation.Show();
        }

        private async Task OnConfirmationChanged(bool deleteConfirmed)
        {
            if (!deleteConfirmed) return;
            var result = await TaskApiClient.DeleteTask(TaskItem.Id);
            if (result)
            {
                ToastService.ShowSuccess("Xóa dữ liệu thành công !", "Thống báo");
                await GetTasks();
            }
            else
            {
                ToastService.ShowError("Xóa dữ liệu chưa thành công !", "Thống báo");
            }
        }

        public void OpenAssignPopup(Guid id)
        {
            AssignTaskDialog.Show(id);
        }

        public async Task AssignTaskSuccess(bool result)
        {
            if (result)
            {
                await GetTasks();
            }
        }

        private async Task GetTasks()
        {
            try
            {
                var pagingResponse = await TaskApiClient.GetTaskList(TaskListSearch);
                Tasks = pagingResponse.Items;
                MetaData = pagingResponse.MetaData;
            }
            catch (Exception ex)
            {
                //Error.ProcessError(ex);
            }

        }

        private async Task SelectedPage(int page)
        {
            TaskListSearch.PageNumber = page;
            await GetTasks();
        }

    }
}
