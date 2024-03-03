using HalloDocs.Repositories.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static System.Runtime.InteropServices.JavaScript.JSType;
using HalloDocs.Repositories.Repository;
using HalloDocs.Entities.DataContext;
using HalloDocs.Entities.DataModels;
using HalloDocs.Entities.ViewModels;
using Microsoft.AspNetCore.Http;


namespace HalloDocs.Controllers
{
    public class AdminController : Controller
    {
        private readonly IAdminRepository _admin;
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context, IAdminRepository admin)
        {
            _context = context;
            _admin = admin;
        }

        [Route("/Admin/AdminDashboard",Name="AdminDashboard") ]
        public IActionResult AdminDashboard()
        {
            AdminDashboardViewModel cnt = new AdminDashboardViewModel();
            cnt.countNew = _context.RequestClients.Include(u => u.Request).Where(u => u.Request.Status == 1).Count();
            cnt.countPending = _context.RequestClients.Include(u => u.Request).Where(u => u.Request.Status == 2).Count();
            cnt.countActive = _context.RequestClients.Include(u => u.Request).Where(u => u.Request.Status == 3 ).Count();
            cnt.countConclude = _context.RequestClients.Include(u => u.Request).Where(u => u.Request.Status == 4).Count();
            cnt.countToClose = _context.RequestClients.Include(u => u.Request).Where(u => u.Request.Status == 5 ).Count();
            cnt.countUnpaid = _context.RequestClients.Include(u => u.Request).Where(u => u.Request.Status == 6).Count();
            cnt.regions=_context.Regions.ToList();
            return View(cnt);

        }

        public IActionResult PatientStatusNew()
        {
            List<AdminDashboardViewModel> newData = _admin.GetNewRequest();
            return PartialView("_PatientNew", newData);
        }

        //[Route("/Admin/ViewCase", Name="ViewCase")]
        public IActionResult ViewCase(int reqId)
        {
            var newData = _admin.ViewCase(reqId);
            return PartialView("_ViewCase", newData);
        }



        public IActionResult PatientStatusActive()
        {
            List<AdminDashboardViewModel> newData = _admin.GetActiveRequest();
            return PartialView("_PatientActive", newData);
        }

        public IActionResult PatientStatusPending()
        {
            List<AdminDashboardViewModel> newData = _admin.GetPendingRequest();
            return PartialView("_PatientPending", newData);
        }

        public IActionResult PatientStatusConclude()
        {
            List<AdminDashboardViewModel> newData = _admin.GetConcludeRequest();
            return PartialView("_PatientConclude", newData);
        }

        public IActionResult PatientStatusToClose()
        {
            List<AdminDashboardViewModel> newData = _admin.GetToCloseRequest();
            return PartialView("_PatientToClose", newData);
        }

        public IActionResult PatientStatusUnpaid()
        {
            List<AdminDashboardViewModel> newData = _admin.GetUnPaidRequest();
            return PartialView("_PatientUnpaid", newData);
        }
     

        public IActionResult ProviderLocation()
        {
          return View();

        }
        public IActionResult Profile()
        {
            return View();

        }
        public IActionResult Providers()
        {
            return View();

        }
        public IActionResult Partners()
        {
            return View();

        }
        public IActionResult Access()
        {
            return View();

        }
        public IActionResult Records()
        {
            return View();
        }
    
    }
}
