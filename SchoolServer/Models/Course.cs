using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SchoolServer.Models;

public partial class Course
{
    [Key]
    [Column("course_id")]
    public int CourseId { get; set; }

    [Column("course_name")]
    [StringLength(255)]
    [Unicode(false)]
    public string CourseName { get; set; } = null!;

    [Column("course_description", TypeName = "text")]
    public string CourseDescription { get; set; } = null!;

    [Column("instructor_name")]
    [StringLength(255)]
    [Unicode(false)]
    public string? InstructorName { get; set; }

    [InverseProperty("Course")]
    public virtual ICollection<Student> Students { get; set; } = new List<Student>();
}
