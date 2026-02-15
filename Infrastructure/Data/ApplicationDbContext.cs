using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<Exam> Exams { get; set; }
    public DbSet<Lesson> Lessons { get; set; }
    public DbSet<Question> Questions { get; set; }
    public DbSet<StudentCourse> StudentCourses { get; set; }
    public DbSet<StudentExamResult> StudentExamResults { get; set; }
    public DbSet<AnswerOption> AnswerOptions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique();
        modelBuilder.Entity<StudentExamResult>()
            .HasIndex(x => new { x.StudentId, x.ExamId }).IsUnique();
        modelBuilder.Entity<StudentCourse>()
            .HasIndex(x => new { x.StudentId, x.CourseId }).IsUnique();
        
        modelBuilder.Entity<User>().HasMany(x => x.StudentExamResults).WithOne(x => x.Student);
        modelBuilder.Entity<User>().HasMany(x => x.TeacherCourses).WithOne(x => x.Teacher);
        modelBuilder.Entity<User>().HasMany(x => x.StudentCourses).WithOne(x => x.Student);

        modelBuilder.Entity<Course>().HasMany(x => x.Exams).WithOne(x => x.Course);
        modelBuilder.Entity<Course>().HasMany(x => x.Lessons).WithOne(x => x.Course);
        modelBuilder.Entity<Course>().HasMany(x => x.Students).WithOne(x => x.Course);

        modelBuilder.Entity<Exam>().HasMany(x => x.Questions).WithOne(x => x.Exam);
        modelBuilder.Entity<Exam>().HasMany(x => x.StudentExamResults).WithOne(x => x.Exam);
        
        modelBuilder.Entity<Question>().HasMany(x => x.AnswerOptions).WithOne(x => x.Question);
    }
}