﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace HalloDocs.Entities.DataModels;

[Table("RequestConcierge")]
public partial class RequestConcierge
{
    [Key]
    public int Id { get; set; }

    public int RequestId { get; set; }

    public int ConciergeId { get; set; }

    [Column("IP")]
    [StringLength(20)]
    public string? Ip { get; set; }

    [ForeignKey("ConciergeId")]
    [InverseProperty("RequestConcierges")]
    public virtual Concierge Concierge { get; set; } = null!;
}
