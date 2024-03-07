using HalloDocs.Entities.ViewModels;
using HalloDocs.Entities.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace HalloDocs.Repositories.Repository.Interface
{
    public interface IAdminRepository
    {
        public AspNetUser AdminLogin(AdminLoginViewModel model, HttpContext httpContext);
        public string AdminLogOut(HttpContext httpContext);
        public List<AdminDashboardViewModel> GetNewRequest();

        public List<AdminDashboardViewModel> GetPendingRequest();

        public List<AdminDashboardViewModel> GetActiveRequest();

        public List<AdminDashboardViewModel> GetConcludeRequest();

        public List<AdminDashboardViewModel> GetToCloseRequest();

        public List<AdminDashboardViewModel> GetUnPaidRequest();

        public object GetViewCase(ViewCaseViewModel model, int reqId);

    }
}
