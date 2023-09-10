using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Common.DALModels;


[Table("Genre")]
public partial class Genre
{
    [Key]
    public int Id { get; set; }

    [StringLength(256)]
    public string Name { get; set; } = null!;

    [StringLength(1024)]
    public string? Description { get; set; }

    [InverseProperty("Genre")]
    public virtual ICollection<Video> Videos { get; set; } = new List<Video>();
}
