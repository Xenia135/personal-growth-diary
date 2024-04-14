using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Planer1.Models;

public partial class Circleoflife
{
    [Key]
    public int IdSector { get; set; }

    [Display(Name = "Пользователь")]
    public int IdUsers { get; set; }

    
    [Column(TypeName = "varchar(40)")]
    [StringLength(40)]
    [Display(Name = "Название")]
    public string Namesector { get; set; } = null!;

    [Range(0, 10)]
    [Column(TypeName = "numeric(2)")]
    [Display(Name = "Оценка")]
    public decimal Fullness { get; set; }

    [Required]
    [Display(Name = "Пользователь")]
    public virtual Useal IdUsersNavigation { get; set; } = null!;

    public virtual ICollection<Purpose> Purposes { get; set; } = new List<Purpose>();
}
