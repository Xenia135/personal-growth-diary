using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Planer1.Models;

public partial class Reminder
{
    [Key]
    public int IdReminder { get; set; }

    [Required]
    public int IdStage { get; set; }

    [Required]
    [Display(Name = "День")]
    public DateOnly Day { get; set; }

    [Required]
    [Display(Name = "Время")]
    public TimeOnly Time { get; set; }

    [Required]
    [Display(Name = "Название привычки")]
    public virtual Stage IdStageNavigation { get; set; } = null!;
}
