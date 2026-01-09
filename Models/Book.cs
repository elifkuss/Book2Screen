using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Book2Screen.Models;

public partial class Book
{
    [Display(Name = "Book ID")]
    public int BookId { get; set; }

    [Display(Name = "Genre")]
    public string? Genre { get; set; }

    [Display(Name = "Name")]
    public string? Name { get; set; }

    [Display(Name = "Summary")]
    public string? Summary { get; set; }

    [Display(Name = "Original Language")]
    public string? Orjlanguage { get; set; }

    [Display(Name = "Country")]
    public string? Country { get; set; }

    [Display(Name = "Author ID")]
    public int? AuthorId { get; set; }

    
    public virtual Author? Author { get; set; }

    public virtual ICollection<Comment> Comments { get; } = new List<Comment>();

    public virtual ICollection<Movie> Movies { get; } = new List<Movie>();
}
