using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Book2Screen.Models;

public partial class User 
{
    public int Id { get; set; }

    [Display(Name = "Name")]
    public string? Name { get; set; }

    [Display(Name = "Email")]
    public string? Mail { get; set; }

    public virtual ICollection<Comment> Comments { get; } = new List<Comment>();
}
