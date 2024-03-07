
using HalloDocs.Repositories.Repository;
using HalloDocs.Repositories.Repository.Interface;
using HalloDocs.Entities.DataContext;
using HalloDocs.Entities.DataModels;
using HalloDocs.Entities.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Diagnostics;
using System.Net.Http;
using System.IO.Compression;
using HalloDocs.Service.Interfaces;
using HalloDocs.Auth;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace HalloDocs.Controllers
{
    public class PatientController : Controller
    {
        private readonly IPatientRepository _patient;
        private readonly ApplicationDbContext _context;
        private readonly IJwtService _jwtService;

        public PatientController(IPatientRepository patient, ApplicationDbContext context,IJwtService jwtService)
        {
            _patient = patient;
            _context = context;
            _jwtService = jwtService;
        }


        [Route("/")]
        [Route("Patient", Name = "PatientSite")]
        public IActionResult PatientSite()
        {
            return View();
        }

        [Route("Patient/RequestTypes", Name = "SubmitRequestTypes")]
        public IActionResult SubmitRequestTypes()
        {
            return View();

        }

        [HttpGet]
        [Route("patient/PatientsTypeRequest", Name = "PatientsTypeRequest")]
        public IActionResult PatientsTypeRequest()
        {
            return View();
        }

        [HttpPost]
        [Route("Patient/PatientsTypeRequest", Name = "PatientsTypeRequest")]
        public IActionResult PatientsTypeRequest(PatientRequestViewModel model)
        {
            if (ModelState.IsValid)
            {

                var result = _patient.PatientsTypeRequest(model);
                if (result == "Success")
                {
                    return RedirectToAction("PatientSite", "Patient");
                }
                else
                {
                    return View();
                }
            }
            else
            {
                return View();
            }

        }

        [HttpGet]
        [Route("/Patient/PatientSubmitRequest/CheckEmail/{email}")]
        public IActionResult CheckEmail(string email)
        {
            var emailExists = _patient.CheckEmailExists(email);
            return Json(new { exists = emailExists });
        }
        
        [HttpGet]
        [Route("Patient/FamilyTypeRequest", Name = "FamilyTypeRequest")]
        public IActionResult FamilyTypeRequest()
        {
            return View();
        }

        [HttpPost]
        [Route("Patient/FamilyTypeRequest", Name = "FamilyTypeRequest")]
        public IActionResult FamilyTypeRequest(FamilyFriendRequestViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = _patient.FamilyTypeRequest(model);
                if (result == "Success")
                {
                    return RedirectToAction("PatientSite", "Patient");
                }
                else
                {
                    return View();
                }
            }
            else
            {
                return View();
            }

        }


        [HttpGet]
        [Route("Patient/ConciergeTypeRequest", Name = "ConciergeTypeRequest")]
        public IActionResult ConciergeTypeRequest()
        {
            return View();
        }

        [HttpPost]
        [Route("Patient/ConciergeTypeRequest", Name = "ConciergeTypeRequest")]
        public IActionResult ConciergeTypeRequest(ConciergeRequestViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = _patient.ConciergeTypeRequest(model);
                if (result == "Success")
                {
                    return RedirectToAction("PatientSite", "Patient");
                }
                else
                {
                    return View();
                }
            }
            else
            {
                return View();
            }

        }

        [HttpGet]
        [Route("Patient/BussinesTypeRequest", Name = "BussinesTypeRequest")]
        public IActionResult BussinesTypeRequest()
        {
            return View();
        }

        [HttpPost]
        [Route("Patient/BussinesTypeRequest", Name = "BussinesTypeRequest")]
        public IActionResult BussinesTypeRequest(BussinessRequestViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = _patient.BussinessTyperequest(model);

                if (result == "Success")
                {
                    return RedirectToAction("PatientSite", "Patient");
                }
                else
                {
                    return View();
                }
            }
            else
            {
                return View();
            }

        }


        [HttpGet]
        [Route("Patient/PatientRegisterPage", Name = "PatientRegisterPage")]
        public IActionResult PatientRegisterPage()
        {
            return View();
        }
        [HttpPost]
        [Route("Patient/PatientRegisterPage", Name = "PatientRegisterPage")]
        public IActionResult PatientRegisterPage(PatientRegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = _patient.PatientRegisterPage(model);

                if (result == "Success")
                {
                    return RedirectToAction("PatientLoginPage", "Patient");
                }
                else
                {
                    ModelState.AddModelError(nameof(model.Email), "User already exists");
                    return View(model);
                }
            }
            return View();

        }
        [HttpGet]
        [Route("Patient/PatientLoginPage", Name = "PatientLoginPage")]
        public IActionResult PatientLoginPage()
        {
            return View();
        }

      
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Patient/PatientLoginPage", Name = "PatientLoginPage")]
        public IActionResult PatientLoginPage(PatientLoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var ans = _patient.PatientLoginPage(model, HttpContext);

                if (ans != null )
                {
                    var token = _jwtService.GenerateJwtToken(ans);
                    Response.Cookies.Append("jwt", token, new CookieOptions
                    {
                        Secure = true,
                        Expires = DateTime.UtcNow.AddHours(1),
                    });
                    TempData["success"] = "User LogIn Successfully";
                    return RedirectToAction("PatientDashboard", "Patient");
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

        public IActionResult PatienLogout(string returnUrl)
        {
            var ans = _patient.PatienLogout(HttpContext);

            if(ans == "Logeout")
            {
                return RedirectToAction("PatientSite", "Patient");
            }
            return View();
        }

        [CustomAuthorization("Patient")]
        [HttpGet]
        [Route("Patient/PatientDashboard", Name = "PatientDashboard")]
        public IActionResult PatientDashboard(PatientDashboardViewModel model)
        {
            var ans = _patient.PatientDashboard(model, HttpContext);
            return View(ans);
        }

        [CustomAuthorization("Patient")]
        [HttpGet]
        [Route("Patient/PatientUserProfile", Name = "PatientUserProfile")]
        public IActionResult PatientUserProfile(PatientDashboardViewModel model)
        {
            var ans = _patient.PatientUserProfile(model, HttpContext);
            return View(ans);
        }

        [HttpPost]
        public IActionResult UpdateUserProfile(PatientDashboardViewModel model)
        {
            var ans = _patient.UpdateUserProfile(model, HttpContext);
            if (ans == "Success")
            {
                TempData["success"] = "Update Profile Succesfully";
                return RedirectToAction("PatientUserProfile", "Patient");
            }
            else
            {
                TempData["error"] = "Invalid Process";
                return RedirectToAction("PatientUserProfile", "Patient");
            }
        }

        [CustomAuthorization("Patient")]
        [HttpGet]
        [Route("Patient/PatientViewDocument/{reqId}", Name = "PatientViewDocument")]
        public IActionResult PatientViewDocument(PatientViewDocumentViewModel model, int reqId)
        {
            var result = _patient.PatientViewDocument(model, reqId, HttpContext);

            return View(result);
        }


        [HttpPost]
        public IActionResult UploadDocuments(PatientViewDocumentViewModel model, int reqId)
        {
            var result = _patient.UploadDocuments(model, reqId);
            if (result == "Success")
            {
                return RedirectToAction("PatientViewDocument", "Patient", new { reqId });

            }
            else
            {
                return View(result);
            }

        }

        /*-----------------------------------------------------SubmitRequestForMe---------------------------------------------------------------------------------*/

        [HttpGet]
        [Route("Patient/PatientDashboard/GetRequestMe", Name = "GetRequestMe")]
        public IActionResult GetRequestMe(PatientCreateNewRequestViewModel model)
        {
            var ans = _patient.GetRequestMe(model, HttpContext);
            return View("RequestMe",ans);
        }

        [HttpGet]
        [Route("Patient/PatientDashboard/RequestMe", Name = "RequestMe")]
        public IActionResult RequestMe()
        {
            
            return View();
        }

        [HttpPost]
        [Route("Patient/PatientDashboard/RequestMe", Name = "RequestMe")]
        public IActionResult RequestMe(PatientCreateNewRequestViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = _patient.RequestMe(model, HttpContext);

                if (result == "Success")
                {
                    return RedirectToAction("PatientDashboard", "Patient");

                }
                else
                {
                    return View(model);
                }
            }
            return View(model);
        }

        [CustomAuthorization("Patient")]
        [HttpGet]
        [Route("Patient/PatientDashboard/RequestSomeOne", Name = "RequestSomeOne")]
        public IActionResult RequestSomeOne()
        {
            return View();
        }
        
        [HttpPost]
        [Route("Patient/PatientDashboard/RequestSomeOne", Name = "RequestSomeOne")]
        public IActionResult RequestSomeOne(PatientCreateNewRequestViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = _patient.RequestSomeOne(model, HttpContext);
                if (result == "Success")
                {
                    return RedirectToAction("PatientDashboard", "Patient");

                }
                else
                {
                    return View(model);
                }
            }
            return View(model);
        }


        ///*--------------------------------------------------------------- Patient  ResetePSW ---------------------------------------------------------------------------------*/
        [HttpGet]
        [Route("Patient/PatientResetePassword", Name = "PatientResetePassword")]
        public IActionResult PatientResetePassword()
        {
            return View();
        }

        [HttpPost]
        [Route("Patient/PatientResetePassword", Name = "PatientResetePassword")]
        public IActionResult PatientResetePassword(PatientResetePasswordViewModel model)
        {
            var userExist = _patient.PatientResetePassword(model);
            if (userExist != null)
            {
                var link = Request.Scheme + "://" + Request.Host + "/Patient/PatientUpdatePassword";
                var msg = SendEmail(model.Email, link);
                TempData["success"] = "Successfully Send On Email Adreess";
                return RedirectToAction("PatientLoginPage");
            }
            else
            {
                ModelState.AddModelError(nameof(model.Email), "Invalid Email Address..");
                TempData["error"] = "Invalid Email Address..";
                return View(model);
            }

        }

        public string SendEmail(string email, string link)
        {
            var result = _patient.SendEmail(email, link);
            return result;
        }

        ///*--------------------------------------------------------------- Patient UpdatePassword Page---------------------------------------------------------------------------------*/
        [HttpGet]
        [Route("Patient/PatientUpdatePassword", Name = "PatientUpdatePassword")]
        public IActionResult PatientUpdatePassword()
        {
            return View();
        }


        [HttpPost]
        [Route("Patient/PatientUpdatePassword", Name = "PatientUpdatePassword")]
        public IActionResult PatientUpdatePassword(PatientUpdatePasswordViewModel model, HttpContext httpContext)
        {
            var result=_patient.PatientUpdatePassword(model,HttpContext);
            if (result == "emailNull")
            {
                return View();
            }
            if (result== "samePassword")
            {
                ModelState.AddModelError(nameof(PatientUpdatePasswordViewModel.PasswordHash), "New password must be different from the current.");
            }
            if (result== "Success")
            {
                TempData["success"] = "Successfully Update your Password ";
                return RedirectToAction("PatientLoginPage", "Patient");
            }
            return View();
        }

        public IActionResult PatientLogout()
        {

            return RedirectToAction("PatientLoginPage");
        }


        /*-----------------------------------------------------Patient AgreementView---------------------------------------------------------------------------------*/

        [HttpGet]
        [Route("Patient/PatientAgreementView", Name = "PatientAgreementView")]
        public IActionResult PatientAgreementView()
        {
            return View();
        }


        public IActionResult Download(int documentid)
        {
            var filename = _context.RequestWiseFiles.FirstOrDefault(u => u.RequestWiseFileId == documentid);

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/UploadedFiles", filename.FileName);
            var downloadFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/DownloadFiles", filename.FileName);
            System.IO.File.Copy(filePath, downloadFilePath);

            return File(System.IO.File.ReadAllBytes(downloadFilePath), "multipart/form-data", filename.FileName);
        }


        public IActionResult DownloadAll(int reqId)
        {
            var files = _context.RequestWiseFiles.Where(u => u.RequestId == reqId).ToList();

            if (files.Any())
            {
                var zipFileName = $"Request_{reqId}_Files.zip";
                var zipFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/DownloadFiles", zipFileName);

                using (var zipStream = new FileStream(zipFilePath, FileMode.Create, FileAccess.Write, FileShare.Write))
                using (var zipArchive = new ZipArchive(zipStream, ZipArchiveMode.Create, true))
                {
                    foreach (var file in files)
                    {
                        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/UploadedFiles", file.FileName);
                        var zipEntry = zipArchive.CreateEntry(file.FileName, CompressionLevel.Fastest);
                        using (var entryStream = zipEntry.Open())
                        {
                            using (var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
                            {
                                fileStream.CopyTo(entryStream);
                            }
                        }
                    }
                }
                return File(System.IO.File.ReadAllBytes(zipFilePath), "application/zip", zipFileName);
            }
            return NotFound();
        }

        public IActionResult AccessDenied()
        {
            return View();
        }

    }
}