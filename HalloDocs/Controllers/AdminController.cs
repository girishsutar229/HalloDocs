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
using Microsoft.Extensions.Primitives;
using HalloDocs.Auth;
using HalloDocs.Service.Interfaces;

namespace HalloDocs.Controllers
{
    public class AdminController : Controller
    {
        private readonly IAdminRepository _admin;
        private readonly ApplicationDbContext _context;
        private readonly IJwtService _jwtService;

        public AdminController(ApplicationDbContext context, IAdminRepository admin, IJwtService jwtService)
        {
            _context = context;
            _admin = admin;
            _jwtService = jwtService;
        }



        [HttpGet]
        [Route("admin/adminLogin", Name = "AdminLogin")]
        public IActionResult AdminLogin()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("admin/adminLogin", Name = "AdminLogin")]
        public IActionResult AdminLogin(AdminLoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var ans = _admin.AdminLogin(model, HttpContext);

                if (ans != null)
                {
                    var token = _jwtService.GenerateJwtToken(ans);
                    Response.Cookies.Append("jwt", token, new CookieOptions
                    {
                        Secure = true,
                        Expires = DateTime.UtcNow.AddHours(1),
                    });
                    TempData["success"] = "Admin LogIn Successfully";
                    return RedirectToAction("AdminDashboard", "Admin");
                }
                else
                {
                    ModelState.AddModelError(nameof(model.PasswordHash), "Invalid  email or Password");
                    TempData["error"] = "Invalid Email or PassWord";
                    return View(model);
                }

            }
            return View(model);
        }

        public IActionResult AdminLogOut(string url)
        {
            var ans = _admin.AdminLogOut(HttpContext);

            if (ans == "Logeout")
            {
                return RedirectToAction("AdminLogin", "Admin");
            }
            return View();
        }


        [Route("admin/adminforgotpassword", Name = "AdminForgotPassword")]
        public IActionResult AdminForgotPassword()
        {
            return View();
        }

        [CustomAuthorization("Admin")]
        [Route("/admin/admindashboard", Name = "AdminDashboard")]
        public IActionResult AdminDashboard()
        {
            AdminDashboardViewModel cnt = new AdminDashboardViewModel();
            cnt.countNew = _context.RequestClients.Include(u => u.Request).Where(u => u.Request.Status == 1).Count();
            cnt.countPending = _context.RequestClients.Include(u => u.Request).Where(u => u.Request.Status == 2).Count();
            cnt.countActive = _context.RequestClients.Include(u => u.Request).Where(u => u.Request.Status == 3).Count();
            cnt.countConclude = _context.RequestClients.Include(u => u.Request).Where(u => u.Request.Status == 4).Count();
            cnt.countToClose = _context.RequestClients.Include(u => u.Request).Where(u => u.Request.Status == 5).Count();
            cnt.countUnpaid = _context.RequestClients.Include(u => u.Request).Where(u => u.Request.Status == 6).Count();
            cnt.regions = _context.Regions.ToList();
            return View(cnt);

        }


        [CustomAuthorization("Admin")]
        [Route("admin/providerloaction", Name = "ProviderLocation")]
        public IActionResult ProviderLocation()
        {
            return View();

        }
        [CustomAuthorization("Admin")]
        [Route("admin/profile", Name = "Profile")]
        public IActionResult Profile()
        {
            return View();

        }

        [CustomAuthorization("Admin")]
        [Route("admin/providers", Name = "Providers")]
        public IActionResult Providers()
        {
            return View();

        }

        [CustomAuthorization("Admin")]
        [Route("admin/partners", Name = "Partners")]
        public IActionResult Partners()
        {
            return View();

        }
        [CustomAuthorization("Admin")]
        [Route("admin/access", Name = "Access")]
        public IActionResult Access()
        {
            return View();

        }

        [CustomAuthorization("Admin")]
        [Route("admin/records", Name = "Records")]
        public IActionResult Records()
        {
            return View();
        }

        [CustomAuthorization("Admin")]
        [Route("admin/patientStatusNew", Name = "PatientStatusNew")]
        public IActionResult PatientStatusNew()
        {
            List<AdminDashboardViewModel> newData = _admin.GetNewRequest();
            return PartialView("_PatientNew", newData);
        }

        [CustomAuthorization("Admin")]
        [Route("admin/patientStatusActive", Name = "PatientStatusActive")]
        public IActionResult PatientStatusActive()
        {
            List<AdminDashboardViewModel> newData = _admin.GetActiveRequest();
            return PartialView("_PatientActive", newData);
        }

        [CustomAuthorization("Admin")]
        [Route("admin/patientStatusPending", Name = "PatientStatusPending")]
        public IActionResult PatientStatusPending()
        {
            List<AdminDashboardViewModel> newData = _admin.GetPendingRequest();
            return PartialView("_PatientPending", newData);
        }

        [CustomAuthorization("Admin")]
        [Route("admin/patientStatusConclude", Name = "PatientStatusConclude")]
        public IActionResult PatientStatusConclude()
        {
            List<AdminDashboardViewModel> newData = _admin.GetConcludeRequest();
            return PartialView("_PatientConclude", newData);
        }

        [CustomAuthorization("Admin")]
        [Route("admin/patientStatusToClose", Name = "PatientStatusToClose")]
        public IActionResult PatientStatusToClose()
        {
            List<AdminDashboardViewModel> newData = _admin.GetToCloseRequest();
            return PartialView("_PatientToClose", newData);
        }

        [CustomAuthorization("Admin")]
        [Route("admin/patientStatusUnpaid", Name = "PatientStatusUnpaid")]
        public IActionResult PatientStatusUnpaid()
        {
            List<AdminDashboardViewModel> newData = _admin.GetUnPaidRequest();
            return PartialView("_PatientUnpaid", newData);
        }

        [CustomAuthorization("Admin")]
        [Route("admin/viewCase", Name = "ViewCase")]
        public IActionResult ViewCase(ViewCaseViewModel model, int reqId)
        {
            var ans = _admin.GetViewCase(model, reqId);
            return View(ans);
        }

        [CustomAuthorization("Admin")]
        [Route("admin/viewNotes", Name = "ViewNotes")]
        public IActionResult ViewNotes(int reqId)
        {
            var requestData = _context.RequestClients.FirstOrDefault(a => a.RequestId == reqId);
            var data = _context.RequestNotes.FirstOrDefault(a => a.RequestId == requestData.RequestId);
            var requestStatusLogData = _context.RequestStatusLogs.FirstOrDefault(a => a.RequestId == reqId);


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
                notesData.adminNotes = data.AdminNotes == null ? "-" :data.AdminNotes;
                notesData.transferNotes = "-";
                notesData.physicianNotes = data.PhysicianNotes == null ? "-" : data.PhysicianNotes;
            }
            return View(notesData);
        }

        [HttpPost]
        public IActionResult AddAdminNotes(int reqId, ViewNotesViewModel model)
        {
            var reqId1 = reqId;
            var data = _context.RequestNotes.FirstOrDefault(a => a.RequestId == reqId);

            if(data == null)
            {
                RequestNote requestNote=new RequestNote();
                requestNote.AdminNotes = model.ViewNotesDescriptions;
                requestNote.CreatedBy = "Admin";
                requestNote.CreatedDate=DateTime.Now;
                requestNote.RequestId = reqId1;
               _context.RequestNotes.Add(requestNote);
               _context.SaveChanges();

            }
            else
            {
                data.AdminNotes = model.ViewNotesDescriptions;
                data.ModifiedBy = "Admin";
                data.ModifiedDate = DateTime.Now;
                _context.RequestNotes.Update(data);
                _context.SaveChanges();
            }

            return RedirectToAction("viewNotes" , new {reqId}); //error
        }

        [CustomAuthorization("Admin")]
        [Route("/admin/cancelModel", Name = "CancelModel")]
        public IActionResult CancelModel(int reqId)
        {
            var requestId = _context.RequestClients.FirstOrDefault(a => a.RequestId == reqId);
            CancleViewModel cancleCaseViewModel = new CancleViewModel();
            cancleCaseViewModel.reqId = reqId;
            cancleCaseViewModel.PatientName = requestId.FirstName + " " + requestId.LastName;
            cancleCaseViewModel.CaseTagsList = _context.CaseTags.ToList();
            return PartialView("_CancelCase", cancleCaseViewModel);
        }

        [HttpPost]
        public IActionResult CancelCase(CancleViewModel model)
        {
            var requestData = _context.Requests.FirstOrDefault(a => a.RequestId == model.reqId);
            requestData.Status = (int)StatusOfRequest.ToClose;
            requestData.CaseTag = model.CaseTags;
            _context.Requests.Update(requestData);
            _context.SaveChanges();

            RequestStatusLog statusLogData = new RequestStatusLog();
            statusLogData.RequestId = model.reqId;
            statusLogData.Status = (int)StatusOfRequest.ToClose;
            statusLogData.CreatedDate = DateTime.Now;
            statusLogData.Notes = model.CancleNotesDescription;
            _context.Add(statusLogData);
            _context.SaveChanges();

            return RedirectToAction("AdminDashboard", "Admin");
        }

        [CustomAuthorization("Admin")]
        [Route("/admin/assignModel", Name = "AssignModel")]
        public IActionResult AssignModel(int reqId)
        {
            var requestId = _context.RequestClients.FirstOrDefault(a => a.RequestId == reqId);
            AssignCaseViewModel assignCaseViewModel = new AssignCaseViewModel();
            assignCaseViewModel.reqId = reqId;
            assignCaseViewModel.physicians = _context.Physicians.ToList();
            assignCaseViewModel.regionList = _context.Regions.ToList();
            return PartialView("_AssignCase", assignCaseViewModel);
        }

        [Route("/admin/physicianByRegion", Name = "PhysicianByRegion")]
        public IActionResult getPhysician(int region)
        {
            var regionData = _context.Regions.Find(region);
            AssignCaseViewModel data = new AssignCaseViewModel();
            data.physicians = _context.Physicians.Where(a => a.RegionId == regionData.RegionId).ToList();
            return Json(data);
        }

        [CustomAuthorization("Admin")]
        public IActionResult AssignCase(AssignCaseViewModel model, int reqId)
        {
            var requestData = _context.Requests.FirstOrDefault(a => a.RequestId == reqId);
            requestData.Status = (int)StatusOfRequest.Pending;
            requestData.PhysicianId = model.PhysicianId;

            _context.Requests.Update(requestData);
            _context.SaveChanges();

            RequestStatusLog statuslog = new RequestStatusLog();
            statuslog.Status = (int)StatusOfRequest.Pending;
            statuslog.RequestId = reqId;
            statuslog.Notes = model.DescriptionsAssignCase;
            statuslog.CreatedDate = DateTime.Now;
            statuslog.TransToPhysicianId = model.PhysicianId;
            _context.RequestStatusLogs.Add(statuslog);
            _context.SaveChanges();
            return RedirectToAction("AdminDashboard", "Admin");

        }

        [CustomAuthorization("Admin")]
        [Route("/admin/blockModel", Name = "BlockModel")]
        public IActionResult BlockModel(int reqId)
        {
            var requestId = _context.RequestClients.FirstOrDefault(a => a.RequestId == reqId);
            BlockPatientViewModel blockPatientViewModel = new BlockPatientViewModel();
            blockPatientViewModel.reqId = reqId;
            blockPatientViewModel.PatientName = requestId.FirstName + " " + requestId.LastName;

            return PartialView("_BlockPatient", blockPatientViewModel);

        }
        [HttpPost]
        [Route("admin/BlockPatient", Name = "BlockPatient")]
        public IActionResult BlockPatient(BlockPatientViewModel model, int reqId)
        {
            var requestData = _context.Requests.FirstOrDefault(a => a.RequestId == reqId);
            requestData.Status = (int)StatusOfRequest.Block;
            _context.Update(requestData);
            _context.SaveChanges();

            BlockRequest blockrequest = new BlockRequest();
            blockrequest.Email = requestData.Email;
            blockrequest.PhoneNumber = requestData.PhoneNumber;
            blockrequest.Reason = model.ResonsBlockRequest;
            blockrequest.RequestId = reqId.ToString();
            blockrequest.CreatedDate = DateTime.Now;
            _context.Add(blockrequest);
            _context.SaveChanges();

            return RedirectToAction("AdminDashboard", "Admin");
        }
       
  
        //RequestClosed closeRequestData = new RequestClosed();
        //closeRequestData.RequestStatusLog = (_context.RequestStatusLogs.Max(a => a.RequestId)) + 1;
        //closeRequestData.RequestId = data.RequestId;
        //closeRequestData.PhyNotes = notes;


        ////[HttpPost]
        ////[Route("admin/GetDataByRegion", Name = "RegionFilter")]
        ////public IActionResult regionFilter()
        ////{
        ////    return View();
        ////}

    }
}
