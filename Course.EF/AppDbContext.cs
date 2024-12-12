using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Course.DAL.Models;

namespace Course.DAL;

public partial class AppDbContext : IdentityDbContext<IdentityUser>
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Answer> Answers { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Comment> Comments { get; set; }

    public virtual DbSet<Models.Course> Courses { get; set; }

    public virtual DbSet<Goal> Goals { get; set; }

    public virtual DbSet<Lesson> Lessons { get; set; }

    public virtual DbSet<Post> Posts { get; set; }

    public virtual DbSet<Question> Questions { get; set; }

    public virtual DbSet<Survey> Surveys { get; set; }

    public virtual DbSet<Unit> Units { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Answer>(entity =>
        {
            entity.ToTable("Answer");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.IsCorrect).HasColumnName("Is_Correct");
            entity.Property(e => e.QuestionId).HasColumnName("Question_Id");

            entity.HasOne(d => d.Question).WithMany(p => p.Answers)
                .HasForeignKey(d => d.QuestionId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_Answer_Question");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.ToTable("Category");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Name).IsUnicode(false);
        });

        modelBuilder.Entity<Comment>(entity =>
        {
            entity.ToTable("Comment");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Comment1)
                .HasMaxLength(600)
                .HasColumnName("Comment");
            entity.Property(e => e.LessonId).HasColumnName("Lesson_Id");
            entity.Property(e => e.ParentCommentId).HasColumnName("Parent_Comment_Id");

            entity.HasOne(d => d.Lesson).WithMany(p => p.Comments)
                .HasForeignKey(d => d.LessonId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_Comment_Lesson");

            entity.HasOne(d => d.ParentComment).WithMany(p => p.InverseParentComment)
                .HasForeignKey(d => d.ParentCommentId)
                .HasConstraintName("FK_Comment_Comment");
        });

        modelBuilder.Entity<Models.Course>(entity =>
        {
            entity.ToTable("Course");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.AllowDownload).HasColumnName("Allow_Download");
            entity.Property(e => e.CategoryId).HasColumnName("Category_Id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("Created_At");
            entity.Property(e => e.HasCertificate).HasColumnName("Has_Certificate");
            entity.Property(e => e.ImageUrl).HasColumnName("Image_URL");

            entity.HasOne(d => d.Category).WithMany(p => p.Courses)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_Course_Category");
        });

        modelBuilder.Entity<Goal>(entity =>
        {
            entity.ToTable("Goal");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CourseId).HasColumnName("Course_Id");

            entity.HasOne(d => d.Course).WithMany(p => p.Goals)
                .HasForeignKey(d => d.CourseId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Goal_Course");
        });

        modelBuilder.Entity<Lesson>(entity =>
        {
            entity.ToTable("Lesson");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.UnitId).HasColumnName("Unit_Id");
            entity.Property(e => e.VideoUrl).HasColumnName("Video_URL");

            entity.HasOne(d => d.Unit).WithMany(p => p.Lessons)
                .HasForeignKey(d => d.UnitId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_Lesson_Unit");
        });

        modelBuilder.Entity<Post>(entity =>
        {
            entity.ToTable("Post");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CategoryId).HasColumnName("Category_Id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("Created_At");
            entity.Property(e => e.ImageUrl).HasColumnName("Image_URL");

            entity.HasOne(d => d.Category).WithMany(p => p.Posts)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_Post_Category");
        });

        modelBuilder.Entity<Question>(entity =>
        {
            entity.ToTable("Question");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.SurveyId).HasColumnName("Survey_Id");

            entity.HasOne(d => d.Survey).WithMany(p => p.Questions)
                .HasForeignKey(d => d.SurveyId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_Question_Survey");
        });

        modelBuilder.Entity<Survey>(entity =>
        {
            entity.ToTable("Survey");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.UnitId).HasColumnName("Unit_Id");

            entity.HasOne(d => d.Unit).WithMany(p => p.Surveys)
                .HasForeignKey(d => d.UnitId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_Survey_Unit");
        });

        modelBuilder.Entity<Unit>(entity =>
        {
            entity.ToTable("Unit");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CourseId).HasColumnName("Course_Id");

            entity.HasOne(d => d.Course).WithMany(p => p.Units)
                .HasForeignKey(d => d.CourseId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_Unit_Course");
        });

        OnModelCreatingPartial(modelBuilder);
        base.OnModelCreating(modelBuilder);

    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
