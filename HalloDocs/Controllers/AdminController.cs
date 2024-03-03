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
using HalloDocs.Entities.Enums;


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


        [Route("admin/login", Name = "AdminLogin")]
        public IActionResult AdminLogin()
        {
            
                return View();           
        }

        [Route("admin/adminforgotpassword", Name = "AdminForgotPassword")]
        public IActionResult AdminForgotPassword()
        {
            return View();
        }



        [Route("/admin/admindashboard",Name="AdminDashboard") ]
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

     

        [Route("admin/providerloaction",Name="ProviderLocation")]
        public IActionResult ProviderLocation()
        {
          return View();

        }
        [Route("admin/profile", Name = "Profile")]
        public IActionResult Profile()
        {
            return View();

        }
        [Route("admin/providers", Name = "Providers")]
        public IActionResult Providers()
        {
            return View();

        }
        [Route("admin/partners", Name = "Partners")]
        public IActionResult Partners()
        {
            return View();

        }
        [Route("admin/access", Name = "Access")]
        public IActionResult Access()
        {
            return View();

        }
        [Route("admin/records", Name = "Records")]
        public IActionResult Records()
        {
            return View();
        }


        public IActionResult PatientStatusNew()
        {
            List<AdminDashboardViewModel> newData = _admin.GetNewRequest();
            return PartialView("_PatientNew", newData);
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


        [Route("admin/viewCase", Name = "ViewCase")]
        public IActionResult ViewCase(ViewCaseViewModel model,int reqId)
        {
            var ans = _admin.GetViewCase(model,reqId);
            return View(ans);
        }

        [Route("admin/viewnotes", Name = "ViewNotes")]
        public IActionResult ViewNotes(int reqId)
        {
            var requestData = _context.RequestClients.FirstOrDefault(a => a.RequestClientId == reqId);
            var data = _context.RequestNotes.FirstOrDefault(a => a.RequestId == requestData.RequestId);

            ViewNotesViewModel notesData = new ViewNotesViewModel();
            if (data == null)
            {
                notesData.requestId = requestData.RequestId;
                notesData.adminNotes = "-";
                notesData.physicianNotes = "-";
                notesData.transferNotes = "-";
            }
            else
            {
                notesData.requestId = requestData.RequestId;
                notesData.requestId = requestData.RequestId;
                notesData.adminNotes = data.AdminNotes;
                notesData.physicianNotes = data.PhysicianNotes == null ? "-" : data.PhysicianNotes;
            }
            return View(notesData);
        }

        [HttpPost]
        [Route("admin/CancelCase", Name = "CancelCase")]
        public IActionResult CancelCase(int reqId, string reason, string notes)
        {
            var data = _context.Requests.FirstOrDefault(a => a.RequestId == reqId);
            data.Status = (int)StatusOfRequest.ToClose;
            _context.Requests.Update(data);
            _context.SaveChanges();

            RequestStatusLog statusLogData = new RequestStatusLog();
            statusLogData.RequestId = data.RequestId;
            statusLogData.Status = (int)StatusOfRequest.ToClose;
            statusLogData.CreatedDate = DateTime.Now;
            _context.Add(statusLogData);
            _context.SaveChanges();

            //RequestClosed closeRequestData = new RequestClosed();
            //closeRequestData.RequestStatusLog = (_context.RequestStatusLogs.Max(a => a.RequestId)) + 1;
            //closeRequestData.RequestId = data.RequestId;
            //closeRequestData.PhyNotes = notes;

            RequestNote notesData = new RequestNote();
            notesData.CreatedDate = DateTime.Now;
            notesData.CreatedBy = "Admin";
            notesData.RequestId = data.RequestId;
            notesData.AdminNotes = reason;
            _context.RequestNotes.Add(notesData);
            _context.SaveChanges();
            return Json(new { success = true, message = "Case canceled successfully." });
        }

        //[HttpPost]
        //[Route("admin/GetDataByRegion", Name = "RegionFilter")]
        //public IActionResult regionFilter()
        //{
        //    return View();
        //}

        [HttpPost]
        [Route("admin/BlockCase", Name = "BlockCase")]
        public IActionResult BlockCase(int reqId, string reason, string email, string phone)
        {
            var requestData = _context.Requests.FirstOrDefault(a => a.RequestId == reqId);
            requestData.Status = (int)StatusOfRequest.Block;
            _context.Update(requestData);
            _context.SaveChanges();

            BlockRequest blockrequest = new BlockRequest();
            blockrequest.Email = email;
            blockrequest.PhoneNumber = phone;
            blockrequest.Reason = reason;
            blockrequest.RequestId = reqId.ToString();
            blockrequest.CreatedDate = DateTime.Now;
            _context.Add(blockrequest);
            _context.SaveChanges();

            return Json(new { success = true, message = "Case Blocked Successfully.." });
        }

    }
}
