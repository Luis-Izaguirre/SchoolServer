using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SchoolServer.Models;

public partial class Student
{
    [Key]
    [Column("student_id")]
    public int StudentId { get; set; }

    [Column("student_name")]
    [StringLength(255)]
    [Unicode(false)]
    public string StudentName { get; set; } = null!;

    [Column("student_email")]
    [StringLength(255)]
    [Unicode(false)]
    public string StudentEmail { get; set; } = null!;

    [Column("major")]
    [StringLength(100)]
    [Unicode(false)]
    public string Major { get; set; } = null!;

    [Column("student_year")]
    public int? StudentYear { get; set; }

    [Column("course_id")]
    public int CourseId { get; set; }

    [Column("population")]
    public int Population { get; set; }

    [ForeignKey("CourseId")]
    [InverseProperty("Students")]
    public virtual Course Course { get; set; } = null!;
}
