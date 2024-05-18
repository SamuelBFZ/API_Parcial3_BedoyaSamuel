using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;
using TaskMangment.DAL.Entities;

namespace TaskMangment.DAL
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)//conexion a la BD
        {

        }

        public DbSet<Tasks> Tasks { get; set; }
    }
}
