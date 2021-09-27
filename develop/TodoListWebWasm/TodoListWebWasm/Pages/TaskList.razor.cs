﻿using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using TodoList.Models;
using TodoList.Models.Enums;
using TodoListWebWasm.Services;

namespace TodoListWebWasm.Pages
{
    public partial class TaskList
    {
        [Inject] private ITaskApiClient TaskApiClient { get; set; }
        [Inject] private IUserApiClient UserApiClient { get; set; }
        [Inject] private IToastService ToastService { get; set; }

        private List<TaskDto> Tasks;
        private List<AssigneeDto> Assignees;
        private TaskListSearch TaskListSearch = new TaskListSearch();

        protected override async Task OnInitializedAsync()
        {
            Tasks = await TaskApiClient.GetTaskList(TaskListSearch);
            Assignees = await UserApiClient.GetUserList();
        }

        private async Task SearchForm(EditContext context)
        {
            //ToastService.ShowInfo("Thông là thông báo !","Info");
            ToastService.ShowSuccess("Thông là thông báo !", "Info");
            ToastService.ShowToast(ToastLevel.Info, "Thông là thông báo !", "Info");
            ToastService.ShowWarning("Thông là thông báo !", "Info");
            ToastService.ShowError("Thông là thông báo !", "Info");

            //Tasks = await TaskApiClient.GetTaskList(TaskListSearch);
        }
    }
}
