using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Book2Screen.Models;

public partial class Actor
{
    
    public int ActorsId { get; set; }

    [Display(Name = "Name")]
    public string? Name { get; set; }

    [Display(Name = "Birthdate")]
    public DateOnly? Birthdate { get; set; }

    [Display(Name = "Movie ID")]
    public int? MovieId { get; set; }

    [Display(Name = "Movie")]
    public virtual Movie? Movie { get; set; }
}
