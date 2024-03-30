using System;
using System.Collections.Generic;

namespace SchoolServer.Models;

public partial class Course
{
    public int CourseId { get; set; }

    public string CourseName { get; set; } = null!;

    public string CourseDescription { get; set; } = null!;

    public string? InstructorName { get; set; }

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();
}
