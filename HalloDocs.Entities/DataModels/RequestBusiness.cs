﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace HalloDocs.Entities.DataModels;

[Table("RequestBusiness")]
public partial class RequestBusiness
{
    [Key]
    public int RequestBusinessId { get; set; }

    public int RequestId { get; set; }

    public int BusinessId { get; set; }

    [Column("IP")]
    [StringLength(20)]
    public string? Ip { get; set; }

    [ForeignKey("BusinessId")]
    [InverseProperty("RequestBusinesses")]
    public virtual Business Business { get; set; } = null!;
}
