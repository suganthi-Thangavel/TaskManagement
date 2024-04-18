using Microsoft.EntityFrameworkCore;
using TaskManagementService.Model;

namespace TaskManagementService.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // DbSet for Project entity
        public DbSet<ProjectModel> Projects { get; set; }

        public DbSet<TaskModel> Tasks { get; set; }
        public DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure your model relationships or constraints here if needed

        //modelBuilder.Entity<TaskModel>()
        //    .HasOne(t => t.Project)             
        //    .WithMany(p => p.Tasks)             
        //    .HasForeignKey(t => t.ProjectId);

        modelBuilder.Entity<ProjectModel>()
            .Property(p => p.CreatedAt)
            .HasDefaultValueSql("GETDATE()");

        modelBuilder.Entity<ProjectModel>()
            .Property(p => p.UpdatedAt)
            .HasDefaultValue(null);

        modelBuilder.Entity<TaskModel>()
            .Property(t => t.CreatedAt)
            .HasDefaultValueSql("GETDATE()");

        modelBuilder.Entity<TaskModel>()
            .Property(t => t.UpdatedAt)
            .HasDefaultValue(null);
        }
    }
}
