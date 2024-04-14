using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Planer1.Models;

public partial class Treker
{
    [Key]
    [Display(Name = "Номер дня")]
    public int IdTreker { get; set; }

    [Display(Name = "Привычка")]
    [Required]
    public int IdStage { get; set; }

    [Display(Name = "Дата")]
    [Required]
    public DateOnly Day { get; set; }

    [Display(Name = "Статус")]
    public bool? Status { get; set; }

    [Key]
    [Display(Name = "Привычка")]
    public virtual Stage IdStageNavigation { get; set; } = null!;
}
