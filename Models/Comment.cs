using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Book2Screen.Models;

public partial class Comment
{
    internal int movieId;

    public int CommentId { get; set; }

    [Display(Name = "Comment Text")]
    public string? CommentText { get; set; }

    [Display(Name = "Movie ID")]
    public int? MovieId { get; set; }

    [Display(Name = "Book ID")]
    public int? BookId { get; set; }

    [Display(Name = "User ID")]
    public int? Id { get; set; }

    public virtual Book? Book { get; set; }

    public virtual User? IdNavigation { get; set; }

    public virtual Movie? Movie { get; set; }
}
