using HalloDocs.Entities.DataModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HalloDocs.Entities.ViewModels
{
    public class PatientDashboardViewModel
    {
        public User User { get; set; } = new User();

        public UserProfileEditedViewModel ProfileEdited { get; set; } = new UserProfileEditedViewModel();

        public IEnumerable<Request> RequestsData { get; set; }

        public IEnumerable<RequestClient> RequestsClientData { get; set; }

        public List<IFormFile> formFile { get; set; }

    }
}
