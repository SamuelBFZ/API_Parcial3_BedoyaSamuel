using TaskMangment.DAL.Entities;

namespace TaskMangment.Domain.Interfaces
{
    public interface ITasksService
    {
        Task<IEnumerable<Tasks>> GetTasksAsync();//Todas las tareas

        Task<Tasks> GetTasksByIdAsync(Guid id);//una tarea por id

        Task<Tasks> GetTasksByDueDateAsync(DateTime date);//Tareas por fecha de vencimiento

        Task<Tasks> CreateTasksAsync(Tasks tasks);

        Task<Tasks> EditTasksAsync(Tasks tasks);

        Task<Tasks> DeleteTasksAsync(Guid id);

    }
}
