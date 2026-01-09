using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Book2Screen.Models;

public partial class Author
{

    public int AuthorId { get; set; }

    [Display(Name = "Name")]
    public string? Name { get; set; }

    [Display(Name = "Birthdate")]
    public DateOnly? Birthday { get; set; }

    [Display(Name = "Works")]
    public string? Works { get; set; }

    [Display(Name = "Country")]
    public string? Country { get; set; }

    public virtual ICollection<Book> Books { get; } = new List<Book>();
}
