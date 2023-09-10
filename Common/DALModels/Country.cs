using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Common.DALModels;

[Table("Country")]
public partial class Country
{
    [Key]
    public int Id { get; set; }

    [StringLength(2)]
    [Unicode(false)]
    public string Code { get; set; } = null!;

    [StringLength(256)]
    public string Name { get; set; } = null!;

    [InverseProperty("CountryOfResidence")]
    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
