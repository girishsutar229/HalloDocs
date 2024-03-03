using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace HalloDocs.Entities.DataModels;

[Table("AdminRegion")]
public partial class AdminRegion
{
    [Key]
    public int AdminRegionId { get; set; }

    public int AdminId { get; set; }

    public int RegionId { get; set; }

    [ForeignKey("RegionId")]
    [InverseProperty("AdminRegions")]
    public virtual Region Region { get; set; } = null!;
}
