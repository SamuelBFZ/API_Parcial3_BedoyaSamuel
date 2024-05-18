using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;
using TaskMangment.DAL;
using TaskMangment.DAL.Entities;
using TaskMangment.Domain.Interfaces;

namespace TaskMangment.Domain.Services
{
    public class TasksService : ITasksService
    {
        private readonly DatabaseContext _context;

        public TasksService(DatabaseContext context)//constructor para inyectar la dependencia
        {
            _context = context;
        }


        public async Task<IEnumerable<Tasks>> GetTasksAsync()//Lista en orden de prioridad
        {
            try
            {
                var tasks = await _context.Tasks.OrderBy(t => t.Priority).ToListAsync();

                return tasks;
            }
            catch (DbUpdateException dbUpdateException)
            {
                throw new Exception(dbUpdateException.InnerException?.Message ?? dbUpdateException.Message);
            }

        }

        public async Task<Tasks> GetTasksByIdAsync(Guid id)//Agarra solo una tarea
        {
            try
            {
                var task = await _context.Tasks.FirstOrDefaultAsync(t => t.Id == id);

                return task;
            }
            catch (DbUpdateException dbUpdateException)
            {
                throw new Exception(dbUpdateException.InnerException?.Message ?? dbUpdateException.Message);
            }
        }

        public async Task<IEnumerable<Tasks>> GetTasksByDueDateAsync(DateTime date)//Puede listar varias tareas
        {
            try
            {
                var task = await _context.Tasks.Where(t => t.DueDate == date).ToListAsync();

                return task;
            }
            catch (DbUpdateException dbUpdateException)
            {
                throw new Exception(dbUpdateException.InnerException?.Message ?? dbUpdateException.Message);
            }
        }

        public async Task<Tasks> CreateTasksAsync(Tasks tasks)//Crea tareas si y solo si tienen una nota de vencimiento mayor a la actual
        {
            if(tasks.DueDate < DateTime.Now.Date)
            {
                throw new ArgumentException("La fecha de vencimiento no puede ser en el pasado.");
            }

            tasks.Id = Guid.NewGuid();
            tasks.CreatedDate = DateTime.Now;
            _context.Tasks.Add(tasks);

            await _context.SaveChangesAsync();

            return tasks;
        }

        public async Task<Tasks> EditTasksAsync(Tasks tasks)
        {
            try
            {
                if (tasks.IsCompleted)//Si la tarea esta completa
                {
                    tasks.CompletionDate = DateTime.Now;
                }

                tasks.ModifiedDate = DateTime.Now;
                _context.Tasks.Update(tasks);
                await _context.SaveChangesAsync();

                return tasks;
            }
            catch (DbUpdateException dbUpdateException)
            {
                throw new Exception(dbUpdateException.InnerException?.Message ?? dbUpdateException.Message);
            }
        }

        public async Task<Tasks> DeleteTasksAsync(Guid id)//Solo permite eliminar tareas si ya estan completadas
        {
            try
            {
                var tasks = await GetTasksByIdAsync(id);

                if (tasks == null)//controlamos que en caso de que el id sea incorrecto o no exista
                {
                    return null;
                }
                else if (!tasks.IsCompleted)
                {
                    throw new InvalidOperationException("La tarea esta aun sin completar.");
                }
                _context.Tasks.Remove(tasks);

                await _context.SaveChangesAsync();

                return tasks;
            }
            catch (DbUpdateException dbUpdateException)
            {
                throw new Exception(dbUpdateException.InnerException?.Message ?? dbUpdateException.Message);
            }
        }

    }
}
