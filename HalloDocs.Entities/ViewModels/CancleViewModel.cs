using HalloDocs.Entities.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HalloDocs.Entities.ViewModels
{
    public class CancleViewModel
    {
        public int reqId { get; set; }

        public string? PatientName { get; set; }

        public string? CancleNotesDescription { get; set;}

        public string? CaseTags { get; set;}

        public List<CaseTag>? CaseTagsList { get; set; } 
    }
}
