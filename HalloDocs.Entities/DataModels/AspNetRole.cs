using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace HalloDocs.Entities.DataModels;

public partial class AspNetRole
{
    [Key]
    public long Id { get; set; }

    [StringLength(256)]
    public string Name { get; set; } = null!;
}
