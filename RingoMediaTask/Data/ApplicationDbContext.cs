
using RingoMediaTask.Data;
using Microsoft.EntityFrameworkCore;

namespace RingoMediaTask.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Reminder> Reminders { get; set; }

        //////////////////////////////////////



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Department>()
        .HasOne(d => d.ParentDepartment)
        .WithMany(d => d.SubDepartments)
        .HasForeignKey(d => d.ParentDepartmentId)
        .OnDelete(DeleteBehavior.NoAction);


        }

    
    }
}





