using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Planer1.Models;

public partial class Stage
{
    [Key]
    public int IdStage { get; set; }

    [Required]
    [Display(Name = "ID Цели")]
    public int IdPurpose { get; set; }

    [Required]
    [StringLength(50)]
    [Column(TypeName = "varchar(50)")]
    [Display(Name = "Название")]
    public string Name { get; set; } = null!;

    [Display(Name = "Статус")]
    public bool? Status { get; set; }

    [Required]
    [Display(Name = "Напоминание")]
    public bool? Reminder { get; set; }

    [Required]
    [Display(Name = "Дата создания")]
    public DateOnly Data { get; set; }

    [Display(Name = "Описание")]
    public string? Description { get; set; }

    [Required]
    [Display(Name = "Цель")]
    public virtual Purpose IdPurposeNavigation { get; set; } = null!;

    public virtual ICollection<Reminder> Reminders { get; set; } = new List<Reminder>();

    public virtual ICollection<Treker> Trekers { get; set; } = new List<Treker>();
}
