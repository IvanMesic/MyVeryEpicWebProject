﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Common.DALModels;

[Table("User")]
public partial class User
{
    [Key]
    public int Id { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    [StringLength(50)]
    public string Username { get; set; } = null!;

    [StringLength(256)]
    public string FirstName { get; set; } = null!;

    [StringLength(256)]
    public string LastName { get; set; } = null!;

    [StringLength(256)]
    public string Email { get; set; } = null!;

    [StringLength(256)]
    public string PwdHash { get; set; } = null!;

    [StringLength(256)]
    public string PwdSalt { get; set; } = null!;

    [StringLength(256)]
    public string? Phone { get; set; }

    public bool IsConfirmed { get; set; }

    [StringLength(256)]
    public string? SecurityToken { get; set; }

    public int CountryOfResidenceId { get; set; }

    [ForeignKey("CountryOfResidenceId")]
    [InverseProperty("Users")]
    public virtual Country CountryOfResidence { get; set; } = null!;
    public string Role;
}
