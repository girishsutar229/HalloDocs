using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HalloDocs.Entities.ViewModels
{
    public class NotesViewModel
    {
        public int RequestId { get; set; }

        public string? TransferNotes { get; set; }

        [Column(TypeName = "timestamp without time zone")]
        public DateTime? CreatedDate { get; set; }

        [StringLength(500)]
        public string? PhysicianNotes { get; set; }

        [StringLength(500)]
        public string? AdminNotes { get; set; }
    }
}
