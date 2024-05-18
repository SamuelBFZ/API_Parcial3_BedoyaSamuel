using System.ComponentModel.DataAnnotations;
using TaskMangment.Enums;

namespace TaskMangment.DAL.Entities
{
    public class Tasks : AuditBase
    {
        [MaxLength(50, ErrorMessage = "El campo {0} es obligatorio")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Nombre")]
        public string Title { get; set; }

        [MaxLength(120, ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Descripcion")]
        public string? Description { get; set; }//no necesariamente todas las tareas deben tener una descripcion

        [Display(Name = "Tarea completa")]
        public bool IsCompleted { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [EnumDataType(typeof(PriorityLevel))]
        [Display(Name = "Prioridad")]
        public string Priority { get; set; }

        [Display(Name = "Fecha limite")]
        public DateTime? DueDate { get; set; }//no necesariamente debe tener una fecha limite

    }
}
