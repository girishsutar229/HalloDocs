using HalloDocs.Entities.DataModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HalloDocs.Entities.ViewModels
{
    public class PatientViewDocumentViewModel
    {
        public IEnumerable<RequestWiseFile>? requestWiseFile { get; set; }
        public Request request { get; set; } = new Request();

        public User User { get; set; } = new User();

        public UserProfileEditedViewModel ProfileEdited { get; set; } = new UserProfileEditedViewModel();

        public RequestClient requestClient { get; set; } = new RequestClient();
        public List<IFormFile>? formFile { get; set; }


       
    }
}
