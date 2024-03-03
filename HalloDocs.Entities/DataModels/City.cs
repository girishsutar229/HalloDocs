using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace HalloDocs.Entities.DataModels;

public partial class City
{
    [Key]
    public int CityId { get; set; }

    [StringLength(50)]
    public string Name { get; set; } = null!;

    public int? RegionId { get; set; }

    [ForeignKey("RegionId")]
    [InverseProperty("Cities")]
    public virtual Region? Region { get; set; }
}
