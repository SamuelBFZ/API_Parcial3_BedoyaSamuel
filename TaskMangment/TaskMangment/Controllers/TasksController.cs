using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Metrics;
using TaskMangment.DAL.Entities;
using TaskMangment.Domain.Interfaces;

namespace TaskMangment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : Controller
    {
        private readonly ITasksService _taskService;

        public TasksController(ITasksService taskService)
        {
            _taskService = taskService;
        }

        [HttpGet]
        [Route("GetAllByOrder")]
        public async Task<ActionResult<IEnumerable<Tasks>>> GetTasksAsync()//Funciona, me trae las tareas en orden de importancia
        {
            var tasks = await _taskService.GetTasksAsync();

            if (tasks == null || !tasks.Any())
            {
                return NotFound();
            }

            return Ok(tasks);
        }

        [HttpGet]
        [Route("GetById")]
        public async Task<ActionResult<Tasks>> GetTasksByIdAsync(Guid id)//Funciona, me trae una tarea especifica por el id
        {
            var tasks = await _taskService.GetTasksByIdAsync(id);

            if (tasks == null)
            {
                return NotFound();
            }

            return Ok(tasks);
        }

        [HttpGet]
        [Route("GetByDueDate")]
        public async Task<ActionResult<Tasks>> GetTasksByGetTasksByDueDateAsync(DateTime date)//Funciona, me trae las tareas segun su fecha de vencimiento, tambien trae repetidas
        {
            var tasks = await _taskService.GetTasksByDueDateAsync(date);

            if (tasks == null)
            {
                return NotFound();
            }

            return Ok(tasks);
        }

        [HttpPost]
        [Route("Create")]
        public async Task<ActionResult<Tasks>> CreateTasksAsync(Tasks tasks)//Funciona, pero el error ejecutado debe ser un 200 y me esta saliendo 500
        {
            var newTasks = await _taskService.CreateTasksAsync(tasks);

            if (newTasks == null) return NotFound();

            return Ok(newTasks);
        }

        [HttpPut]
        [Route("Edit")]
        public async Task<ActionResult<Tasks>> EditTasksAsync(Tasks tasks)//Funciona
        {
                var editedTask = await _taskService.EditTasksAsync(tasks);

                if (editedTask == null) return NotFound();

                return Ok(editedTask);

        }

        [HttpDelete]
        [Route("Delete")]
        public async Task<ActionResult<Tasks>> DeleteTasksAsync(Guid id)//Falta probar
        {
            if (id == null) return BadRequest();

            var deletedTask = await _taskService.DeleteTasksAsync(id);

            if (deletedTask == null) return NotFound();

            return Ok(deletedTask);//Tambien debo cambiar este mensaje
        }
    }
}
