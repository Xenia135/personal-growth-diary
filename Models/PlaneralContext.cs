using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Planer1.Models;

public partial class PlaneralContext : DbContext
{
    public PlaneralContext()
    {
    }

    public PlaneralContext(DbContextOptions<PlaneralContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Circleoflife> Circleoflives { get; set; }

    public virtual DbSet<Purpose> Purposes { get; set; }

    public virtual DbSet<Reminder> Reminders { get; set; }

    public virtual DbSet<Stage> Stages { get; set; }

    public virtual DbSet<Treker> Trekers { get; set; }

    public virtual DbSet<Useal> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Server=localhost;Database=planeral;Username=plan;Password=1357;Persist Security Info=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Circleoflife>(entity =>
        {
            entity.HasKey(e => e.IdSector).HasName("circleoflife_pkey");

            entity.ToTable("circleoflife");

            entity.Property(e => e.IdSector).HasColumnName("id_sector");
            entity.Property(e => e.Fullness)
                .HasPrecision(2)
                .HasColumnName("fullness");
            entity.Property(e => e.IdUsers).HasColumnName("id_users");
            entity.Property(e => e.Namesector)
                .HasMaxLength(40)
                .HasColumnName("namesector");

            entity.HasOne(d => d.IdUsersNavigation).WithMany(p => p.Circleoflives)
                .HasForeignKey(d => d.IdUsers)
                .HasConstraintName("circleoflife_id_users_fkey");
        });

        modelBuilder.Entity<Purpose>(entity =>
        {
            entity.HasKey(e => e.IdPurpose).HasName("purpose_pkey");

            entity.ToTable("purpose");

            entity.Property(e => e.IdPurpose).HasColumnName("id_purpose");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.IdSector).HasColumnName("id_sector");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.Status).HasColumnName("status");

            entity.HasOne(d => d.IdSectorNavigation).WithMany(p => p.Purposes)
                .HasForeignKey(d => d.IdSector)
                .HasConstraintName("purpose_id_sector_fkey");
        });

        modelBuilder.Entity<Reminder>(entity =>
        {
            entity.HasKey(e => e.IdReminder).HasName("reminder_pkey");

            entity.ToTable("reminder");

            entity.Property(e => e.IdReminder).HasColumnName("id_reminder");
            entity.Property(e => e.Day).HasColumnName("day");
            entity.Property(e => e.IdStage).HasColumnName("id_stage");
            entity.Property(e => e.Time).HasColumnName("time");

            entity.HasOne(d => d.IdStageNavigation).WithMany(p => p.Reminders)
                .HasForeignKey(d => d.IdStage)
                .HasConstraintName("reminder_id_stage_fkey");
        });

        modelBuilder.Entity<Stage>(entity =>
        {
            entity.HasKey(e => e.IdStage).HasName("stage_pkey");

            entity.ToTable("stage");

            entity.Property(e => e.IdStage).HasColumnName("id_stage");
            entity.Property(e => e.Data).HasColumnName("data");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.IdPurpose).HasColumnName("id_purpose");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.Reminder).HasColumnName("reminder");
            entity.Property(e => e.Status).HasColumnName("status");

            entity.HasOne(d => d.IdPurposeNavigation).WithMany(p => p.Stages)
                .HasForeignKey(d => d.IdPurpose)
                .HasConstraintName("stage_id_purpose_fkey");
        });

        modelBuilder.Entity<Treker>(entity =>
        {
            entity.HasKey(e => new { e.IdTreker, e.IdStage }).HasName("treker_pkey");

            entity.ToTable("treker");

            entity.Property(e => e.IdTreker)
                .ValueGeneratedOnAdd()
                .HasColumnName("id_treker");
            entity.Property(e => e.IdStage).HasColumnName("id_stage");
            entity.Property(e => e.Day).HasColumnName("day");
            entity.Property(e => e.Status).HasColumnName("status");

            entity.HasOne(d => d.IdStageNavigation).WithMany(p => p.Trekers)
                .HasForeignKey(d => d.IdStage)
                .HasConstraintName("treker_id_stage_fkey");
        });

        modelBuilder.Entity<Useal>(entity =>
        {
            entity.HasKey(e => e.IdUsers).HasName("users_pkey");

            entity.ToTable("users");

            entity.HasIndex(e => e.Mail, "unique_login").IsUnique();

            entity.Property(e => e.IdUsers).HasColumnName("id_users");
            entity.Property(e => e.Mail)
                .HasMaxLength(50)
                .HasColumnName("mail");
            entity.Property(e => e.Name)
                .HasMaxLength(15)
                .HasColumnName("name");
            entity.Property(e => e.Password)
                .HasMaxLength(40)
                .HasColumnName("password");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
