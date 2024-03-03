using HalloDocs.Entities.DataModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HalloDocs.Entities.ViewModels
{
    public class AdminDashboardViewModel
    {
        public IEnumerable<Region> regions { get; set; }

        public int RequestId { get; set; }

        public int RequestTypeId { get; set; }

        public string RequestFirstName { get; set; } = null!;

        public string RequestLastName { get; set; } = null!;

        public string PhysicianName { get; set; } = null!;

        public DateOnly DateOfBirth { get; set; }

        public int? Age { get; set; }

        public string Requestor { get; set; } = null!;

        [StringLength(5)]
        public string? RequestCountryCode { get; set; }

        public string? RequestPhoneNumber { get; set; }

        public DateTime? RequestedDate { get; set; }

        [StringLength(5)]
        public string? ClientCountryCode { get; set; }

        public string? ClientPhoneNumber { get; set; } = null!;

        [StringLength(500)]
        public string? ClientAddress { get; set; }

        public DateTime? DateOfService { get; set; }

        public string? Notes { get; set; }

        public short Status { get; set; }

        public string ClientEmail { get; set; } = null!;

        public string? ClientFirstName { get; set; }

        public string? ClientLastName { get; set; }

        [StringLength(20)]
        public string? ConfirmationNumber { get; set; }

        public int? countNew { get; set; }

        public int? countPending { get; set; }

        public int? countActive { get; set; }

        public int? countConclude { get; set; }

        public int? countToClose { get; set; }

        public int? countUnpaid { get; set; }


    }
}


