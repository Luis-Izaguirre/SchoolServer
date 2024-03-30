﻿using System;
using System.Collections.Generic;

namespace SchoolServer.Models;

public partial class Student
{
    public int StudentId { get; set; }

    public string StudentName { get; set; } = null!;

    public string StudentEmail { get; set; } = null!;

    public string Major { get; set; } = null!;

    public int? StudentYear { get; set; }

    public int CourseId { get; set; }

    public virtual Course Course { get; set; } = null!;
}
