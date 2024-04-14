using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Planer1.Models;

public partial class Useal
{
    [Key]
    public int IdUsers { get; set; }

    [Required]
    [Display(Name = "Пароль")]
    [StringLength(40)]
    [RegularExpression(@"^[A-Z]+[a-zA-Z0-9""'\s-]*$")]
    [Column(TypeName = "varchar(40)")]
    public string Password { get; set; } = null!;

    [Required]
    [Display(Name = "Почта")]
    [StringLength(50)]
    [Column(TypeName = "varchar(50)")]
    public string Mail { get; set; } = null!;

    [Required]
    [Display(Name = "Имя")]
    [StringLength(15)]
    [Column(TypeName = "varchar(15)")]
    public string Name { get; set; } = null!;

    public virtual ICollection<Circleoflife> Circleoflives { get; set; } = new List<Circleoflife>();
}
