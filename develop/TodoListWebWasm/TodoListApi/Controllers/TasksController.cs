using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoListApi.Repositories;
using Task = TodoListApi.Entities.Task;

namespace TodoListApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly ITaskRepository _repository;

        public TasksController(ITaskRepository repository)
        {
            this._repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var tasks = await _repository.GetTaskList();
            return Ok(tasks);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var task = await _repository.GetById(id);
            return Ok(task);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Task task)
        {
            if (!ModelState.IsValid) return BadRequest();

            var taskAdded = await _repository.Create(task);
            return CreatedAtAction(nameof(GetById), new { id = taskAdded.Id }, taskAdded);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] Task task)
        {
            if (!ModelState.IsValid) return BadRequest();

            var taskFind = await _repository.GetById(id);
            if (taskFind == null) return NotFound($"{id} is not found !");

            var taskUpdated = await _repository.Update(taskFind);
            return Ok(taskUpdated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var taskFind = await _repository.GetById(id);
            if (taskFind == null) return NotFound($"{id} is not found !");

            var taskDeleted = await _repository.Delete(taskFind);
            return Ok(taskDeleted);
        }

    }
}
