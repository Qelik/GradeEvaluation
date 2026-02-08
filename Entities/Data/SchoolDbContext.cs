using Entities;
using System.Data.Entity;

public class SchoolDbContext : DbContext
{
    public SchoolDbContext() : base("SchoolDb")
    {
    }

    public DbSet<Teacher> Teachers { get; set; }
    public DbSet<Student> Students { get; set; }
    public DbSet<Exam> Exams { get; set; }
    public DbSet<Task> Tasks { get; set; }

    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Exam>()
            .HasRequired(e => e.Teacher)
            .WithMany(t => t.Exams)
            .HasForeignKey(e => e.TeacherId);

        modelBuilder.Entity<Student>()
            .HasRequired(s => s.Teacher)
            .WithMany(t => t.Students)
            .HasForeignKey(s => s.TeacherId);
    }
}