using System.ComponentModel.DataAnnotations;

namespace TaskMangment.DAL.Entities
{
    public class AuditBase
    {
        [Key]
        [Required]
        public virtual Guid Id { get; set; }
        public virtual DateTime? CreatedDate { get; set; }
        public virtual DateTime? ModifiedDate { get; set; }
        public virtual DateTime? CompletionDate { get; set;  }// En teoria todas las tareas deberian tener su fecha de finalizacion automatica
    }
}
