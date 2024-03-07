using HalloDocs.Entities.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HalloDocs.Entities.ViewModels
{
    public class AssignCaseViewModel
    {
        public int reqId { get; set; }

        public string? DescriptionsAssignCase { get; set; }


        public int PhysicianId { get; set; } 

        public List<Region> regionList { get; set; }

        public List<Physician> physicians { get; set; } = new List<Physician> { };

    }
}
