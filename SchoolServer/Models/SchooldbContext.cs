using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SchoolServer.Models;

public partial class SchooldbContext : DbContext
{
    public SchooldbContext()
    {
    }

    public SchooldbContext(DbContextOptions<SchooldbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Course> Courses { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (optionsBuilder.IsConfigured)
        {
            return;
        }
        IConfigurationBuilder builder1 = new ConfigurationBuilder().AddJsonFile("appsettings.json");
        var config = builder1.Build();
        optionsBuilder.UseSqlServer(config.GetConnectionString("DefaultConnection"));
        //optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Schooldb;Integrated Security=True;");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasOne(d => d.Course).WithMany(p => p.Students)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Students_Courses");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
