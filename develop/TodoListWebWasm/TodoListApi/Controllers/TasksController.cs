using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoListApi.Repositories;

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
    }
}
