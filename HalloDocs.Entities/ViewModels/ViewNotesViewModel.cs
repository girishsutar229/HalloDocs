using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HalloDocs.Entities.ViewModels
{
    public class ViewNotesViewModel
    {
        public int requestId { get; set; }
        public string physicianNotes { get; set; }

        public string adminNotes { get; set; }

        public string transferNotes { get; set; }

    }
}
