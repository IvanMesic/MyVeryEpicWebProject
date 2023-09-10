using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Common.DALModels;

[Table("Notification")]
public partial class Notification
{
    [Key]
    public int Id { get; set; }

    public DateTime CreatedAt { get; set; }

    [StringLength(256)]
    public string ReceiverEmail { get; set; } = null!;

    [StringLength(256)]
    public string? Subject { get; set; }

    [StringLength(1024)]
    public string Body { get; set; } = null!;

    public DateTime? SentAt { get; set; }
}