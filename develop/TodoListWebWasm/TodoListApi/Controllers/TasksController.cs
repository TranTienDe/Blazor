using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoList.Models;
using TodoList.Models.Enums;
using TodoList.Models.SeedWork;
using TodoListApi.Extensions;
using TodoListApi.Repositories;
using Task = TodoListApi.Entities.Task;

namespace TodoListApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class TasksController : ControllerBase
    {
        private readonly ITaskRepository _repository;

        public TasksController(ITaskRepository repository)
        {
            this._repository = repository;
        }

        //api/taks?name=
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] TaskListSearch taskListSearch)
        {
            var pagedList = await _repository.GetTaskList(taskListSearch);
            var taskDtos = pagedList.Items.Select(x => new TaskDto
            {
                Id = x.Id,
                Name = x.Name,
                AssigneeId = x.AssigneeId,
                CreatedDate = x.CreatedDate,
                Priority = x.Priority,
                Status = x.Status,
                AssigneeName = x.Assignee != null ? x.Assignee.FirstName + " " + x.Assignee.LastName : "N/A"
            });
            return Ok(
                new PagedList<TaskDto>(taskDtos.ToList(),
                        pagedList.MetaData.TotalCount,
                        pagedList.MetaData.CurrentPage,
                        pagedList.MetaData.PageSize)
                );
        }

        [HttpGet("me")]
        public async Task<IActionResult> GetByAssigneeId([FromQuery] TaskListSearch taskListSearch)
        {
            var userId = User.GetUserId();
            var pagedList = await _repository.GetTaskListByUserId(Guid.Parse(userId), taskListSearch);
            var taskDtos = pagedList.Items.Select(x => new TaskDto()
            {
                Status = x.Status,
                Name = x.Name,
                AssigneeId = x.AssigneeId,
                CreatedDate = x.CreatedDate,
                Priority = x.Priority,
                Id = x.Id,
                AssigneeName = x.Assignee != null ? x.Assignee.FirstName + ' ' + x.Assignee.LastName : "N/A"
            });

            return Ok(
                    new PagedList<TaskDto>(taskDtos.ToList(),
                        pagedList.MetaData.TotalCount,
                        pagedList.MetaData.CurrentPage,
                        pagedList.MetaData.PageSize)
                );
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var taskFromDb = await _repository.GetById(id);
            return Ok(new TaskDto
            {
                Id = taskFromDb.Id,
                Name = taskFromDb.Name,
                AssigneeId = taskFromDb.AssigneeId,
                CreatedDate = taskFromDb.CreatedDate,
                Priority = taskFromDb.Priority,
                Status = taskFromDb.Status,
            });
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TaskCreateRequest request)
        {
            if (!ModelState.IsValid) return BadRequest();

            var taskAdded = await _repository.Create(new Task
            {
                Id = request.Id,
                Name = request.Name,
                CreatedDate = DateTime.Now,
                Priority = request.Priority.HasValue ? request.Priority.Value : Priority.Low,
                Status = Status.Open
            });
            return CreatedAtAction(nameof(GetById), new { id = taskAdded.Id }, taskAdded);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] TaskUpdateRequest request)
        {
            if (!ModelState.IsValid) return BadRequest();

            var taskFromDb = await _repository.GetById(id);
            if (taskFromDb == null) return NotFound($"{id} is not found !");

            taskFromDb.Name = request.Name;
            taskFromDb.Priority = request.Priority;

            var taskUpdated = await _repository.Update(taskFromDb);

            return Ok(new TaskDto
            {
                Id = taskUpdated.Id,
                Name = taskUpdated.Name,
                AssigneeId = taskUpdated.AssigneeId,
                CreatedDate = taskUpdated.CreatedDate,
                Priority = taskUpdated.Priority,
                Status = taskUpdated.Status,
            });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var taskFind = await _repository.GetById(id);
            if (taskFind == null) return NotFound($"{id} is not found !");

            var taskDeleted = await _repository.Delete(taskFind);
            return Ok(new TaskDto
            {
                Id = taskDeleted.Id,
                Name = taskDeleted.Name,
                AssigneeId = taskDeleted.AssigneeId,
                CreatedDate = taskDeleted.CreatedDate,
                Priority = taskDeleted.Priority,
                Status = taskDeleted.Status,
            });
        }

        [HttpPut]
        [Route("{id}/assign")]
        public async Task<IActionResult> AssignTask(Guid id, [FromBody] AssignTaskRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var taskFromDb = await _repository.GetById(id);

            if (taskFromDb == null)
            {
                return NotFound($"{id} is not found");
            }

            taskFromDb.AssigneeId = request.UserId.Value == Guid.Empty ? null : request.UserId.Value;

            var taskResult = await _repository.Update(taskFromDb);

            return Ok(new TaskDto()
            {
                Name = taskResult.Name,
                Status = taskResult.Status,
                Id = taskResult.Id,
                AssigneeId = taskResult.AssigneeId,
                Priority = taskResult.Priority,
                CreatedDate = taskResult.CreatedDate
            });
        }

    }
}
