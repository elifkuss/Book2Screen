using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Book2Screen.Models;

public partial class Movie
{
    public int MovieId { get; set; }

    [Display(Name = "Movie Title")]
    public string? Title { get; set; }

    [Display(Name = "Rating")]
    public double? Score { get; set; }

    [Display(Name = "Movie Type")]
    public string? Movietype { get; set; }

    [Display(Name = "Release Date")]
    public DateTime? Releasedate { get; set; }

    [Display(Name = "Duration")]
    public int? Movieduration { get; set; }

    [Display(Name = "Budget")]
    public double? Budget { get; set; }

    [Display(Name = "Box Office")]
    public double? Boxofficerevenue { get; set; }

    [Display(Name = "Book ID")]
    public int? BookId { get; set; }

    [Display(Name = "Director ID")]
    public int? DirectorId { get; set; }

    
    public string? Photopath { get; set; }

    public virtual ICollection<Actor> Actors { get; } = new List<Actor>();

    public virtual Book? Book { get; set; }

    public virtual ICollection<Comment> Comments { get; } = new List<Comment>();

    public virtual Director? Director { get; set; }
}
