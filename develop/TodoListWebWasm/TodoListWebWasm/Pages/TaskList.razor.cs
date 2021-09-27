using Microsoft.AspNetCore.Components;
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

        private List<TaskDto> Tasks;
        private List<AssigneeDto> Assignees;
        private TaskListSearch TaskListSearch = new TaskListSearch();

        protected override async Task OnInitializedAsync()
        {
            Tasks = await TaskApiClient.GetTaskList();
            Assignees = await UserApiClient.GetUserList();
        }
    }

    public class TaskListSearch
    {
        public string Name { get; set; }
        public Guid AssigneeId { get; set; }
        public Priority Priority { get; set; }
    }
}
