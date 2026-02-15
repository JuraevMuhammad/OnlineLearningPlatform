namespace Domain.Entities;

public class StudentCourse : BaseEntity
{
    public int StudentId { get; set; }
    public User? Student { get; set; }

    public int CourseId { get; set; }
    public Course? Course { get; set; }
}