using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Planer1.Models;

public partial class Purpose
{
    [Key]
    public int IdPurpose { get; set; }

    public int IdSector { get; set; }

    [Required]
    [StringLength(50)]
    [Column(TypeName = "varchar(50)")]
    [Display(Name = "Название")]
    public string Name { get; set; } = null!;

    [Display(Name = "Статус")]
    public bool? Status { get; set; }

    [Display(Name = "Описание")]
    public string? Description { get; set; }

    [Display(Name = "Сфера жизни")]
    public virtual Circleoflife IdSectorNavigation { get; set; } = null!;

    public virtual ICollection<Stage> Stages { get; set; } = new List<Stage>();
}
