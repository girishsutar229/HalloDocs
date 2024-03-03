using HalloDocs.Entities.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HalloDocs.Entities.ViewModels
{
    public class ViewCaseViewModel
    {
        public RequestClient requestclient { get; set; } = new RequestClient();
        public int status { get; set; }

        public int requestType { get; set; }
        public string ConfirmationNumber { get; set; }

    }
}
