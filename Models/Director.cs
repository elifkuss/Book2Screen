using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Book2Screen.Models;

public partial class Director
{
    public int DirectorId { get; set; }

    [Display(Name = "Name")]
    public string? Name { get; set; }

    [Display(Name = "Birthdate")]
    public DateOnly? Birthdate { get; set; }

    [Display(Name = "Works")]
    public string? Works { get; set; }

    public virtual ICollection<Movie> Movies { get; } = new List<Movie>();
}
