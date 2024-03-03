using HalloDocs.Entities.ViewModels;
using HalloDocs.Repositories.Repository.Interface;
using HalloDocs.Entities.DataContext;
using HalloDocs.Entities.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Net.Http;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using static System.Net.Mime.MediaTypeNames;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.IO.Compression;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.Net.Mail;
using System.Net;

namespace HalloDocs.Repositories.Repository
{
    public class PatientRepository : IPatientRepository
    {
        private readonly ApplicationDbContext _context;
        public PatientRepository(ApplicationDbContext context)
        {
           _context = context;
        }


        public string PatientsTypeRequest(PatientRequestViewModel model)
        {
            var firsttwocharsfromfname = model.FirstName.Substring(0, 2);
            var lasttwocharsfromlname = model.LastName.Substring(0, 2);
            var stateabbr = model.State.Substring(0, 2);
            var date = DateTime.Now.Day.ToString("00");
            var month = DateTime.Now.Month.ToString("00");
            var totalRequests = _context.Requests.Where(r => r.CreatedDate.Date == DateTime.Now.Date).Count().ToString("0000");

            var aspnetuser = _context.AspNetUsers.Any(u => u.Email == model.Email);
            if (!aspnetuser)
            {
                var aspnetuser1 = new AspNetUser
                {
                    UserName = model.FirstName,
                    Email = model.Email,
                    PasswordHash = model?.PasswordHash,
                    CreatedDate = DateTime.Now,
                    PhoneNumber = model.PhoneNumber,
                    ContryCode = model.ContryCode,
                };

                _context.AspNetUsers.Add(aspnetuser1);
                _context.SaveChanges();

                var user = new User
                {
                    AspNetUserId = aspnetuser1.Id,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    DateOfBirth = model.DateOfBirth,
                    IntDate = model.DateOfBirth.Day,
                    IntYear = model.DateOfBirth.Year,
                    StrMonth = model.DateOfBirth.ToString("MMMM"),
                    Mobile = model.PhoneNumber,
                    ContryCode = model.ContryCode,
                    ZipCode = model.ZipCode,
                    State = model.State,
                    City = model.City,
                    Street = model.Street,
                    CreatedBy = "patient",
                    CreatedDate = DateTime.Now,
                };
                _context.Users.Add(user);
                _context.SaveChanges();
            }

            var existingUser = _context.Users.FirstOrDefault(u => u.Email == model.Email); //Existing user cheking for userId
            var request = new Request
            {
                UserId = existingUser?.UserId,
                RequestTypeId = 1,
                FirstName = model.FirstName,
                LastName = model.LastName,
                PhoneNumber = model.PhoneNumber,
                ContryCode = model.ContryCode,
                Email = model.Email,
                CreatedDate = DateTime.Now,
                Status = 4,
                ConfirmationNumber = (stateabbr + date + month + lasttwocharsfromlname + firsttwocharsfromfname + totalRequests).ToUpper(),
            };
            _context.Requests.Add(request);
            _context.SaveChanges();

            var Request = _context.Requests.OrderBy(e => e.RequestId).LastOrDefault(e => e.Email == model.Email);
            var requestClient = new RequestClient
            {
                RequestId = Request.RequestId,
                FirstName = model.FirstName,
                LastName = model.LastName,
                PatientSymptoms = model.Symptoms,
                PhoneNumber = model.PhoneNumber,
                ContryCode = model.ContryCode,
                Email = model.Email,
                DateOfBirth = model.DateOfBirth,
                IntDate = model.DateOfBirth.Day,
                IntYear = model.DateOfBirth.Year,
                StrMonth = model.DateOfBirth.ToString("MMMM"),
                ZipCode = model.ZipCode,
                State = model.State,
                City = model.City,
                Street = model.Street,
                Address =model.Street+ " ,"+ model.City + " ," + model.State + "-" + model.ZipCode,
                Location = model.PatientRoomNumber,
            };

            _context.RequestClients.Add(requestClient);
            _context.SaveChanges();

            //For File Store
            if (model.formFile != null && model.formFile.Count > 0)
            {
                foreach (var file in model.formFile)
                {
                    string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/UploadedFiles", file.FileName);
                    using (var stream = System.IO.File.Create(filePath))
                    {
                        file.CopyTo(stream);

                        var userCheck = _context.Requests.OrderBy(e => e.RequestId).LastOrDefault(e => e.Email == model.Email);
                        RequestWiseFile requestwiseFile = new RequestWiseFile();
                        requestwiseFile.RequestId = userCheck.RequestId;
                        requestwiseFile.FileName = file.FileName;
                        requestwiseFile.CreatedDate = DateTime.Now;
                        requestwiseFile.DocType = 1;

                        _context.RequestWiseFiles.Add(requestwiseFile);
                        _context.SaveChanges();
                    }
                }
            }
            return "Success";
        }
        public bool CheckEmailExists(string email)
        {
            var emailExists = _context.AspNetUsers.Any(u => u.Email == email);
            return emailExists;
        }

        public string FamilyTypeRequest(FamilyFriendRequestViewModel model)
        {
            var firsttwocharsfromfname = model.PatientFirstName.Substring(0, 2);
            var lasttwocharsfromlname = model.PatientLastName.Substring(0, 2);
            var stateabbr = model.PatientState.Substring(0, 2);
            var date = DateTime.Now.Day.ToString("00");
            var month = DateTime.Now.Month.ToString("00");
            var totalRequests = _context.Requests.Where(r => r.CreatedDate.Date == DateTime.Now.Date).Count().ToString("0000");

            var exitinguser = _context.Users.FirstOrDefault(u => u.Email == model.Email);
            var request = new Request
            {
                UserId = exitinguser?.UserId,
                RequestTypeId = 2,
                FirstName = model.FirstName,
                LastName = model.LastName,
                PhoneNumber = model.PhoneNumber,
                ContryCode = model.ContryCode,
                Email = model.Email,
                CreatedDate = DateTime.Now,
                RelationName = model.RelationName,
                Status = 1,
                ConfirmationNumber = (stateabbr + date + month + lasttwocharsfromlname + firsttwocharsfromfname + totalRequests).ToUpper(),
            };
            _context.Requests.Add(request);
            _context.SaveChanges();

            var requestCheck = _context.Requests.OrderBy(e => e.RequestId).LastOrDefault(e => e.Email == model.Email);
            var requestclient = new RequestClient
            {
                RequestId = requestCheck.RequestId,
                FirstName = model.PatientFirstName,
                LastName = model.PatientLastName,
                PatientSymptoms = model.PatientSymptoms,
                PhoneNumber = model.PatientPhoneNumber,
                ContryCode = model.PatientContryCode,
                Email = model.PatientEmail,
                DateOfBirth = model.PatientDateOfBirth,
                IntDate = model.PatientDateOfBirth.Day,
                IntYear = model.PatientDateOfBirth.Year,
                StrMonth = model.PatientDateOfBirth.ToString("MMMM"),
                ZipCode = model.PatientZipCode,
                State = model.PatientState,
                City = model.PatientCity,
                Street = model.PatientStreet,
                Address =model.PatientStreet+ " ,"+ model.PatientCity +" ," + model.PatientState + "-" + model.PatientZipCode,
                Location = model.PatientRoomNumber,

            };
            _context.RequestClients.Add(requestclient);
            _context.SaveChanges();

            //For File Store
            if (model.formFile != null && model.formFile.Count > 0)
            {
                foreach (var file in model.formFile)
                {
                    string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/UploadedFiles", file.FileName);

                    using (var stream = System.IO.File.Create(filePath))
                    {
                        file.CopyTo(stream);

                        var userCheck = _context.Requests.OrderBy(e => e.RequestId).LastOrDefault(e => e.Email == model.Email);
                        RequestWiseFile requestwiseFile = new RequestWiseFile();
                        requestwiseFile.RequestId = userCheck.RequestId;
                        requestwiseFile.FileName = file.FileName;
                        requestwiseFile.CreatedDate = DateTime.Now;
                        requestwiseFile.DocType = 1;

                        _context.RequestWiseFiles.Add(requestwiseFile);
                        _context.SaveChanges();
                    }
                }
            }

            return "Success";

        }

        public string ConciergeTypeRequest(ConciergeRequestViewModel model)
        {
            var firsttwocharsfromfname = model.PatientFirstName.Substring(0, 2);
            var lasttwocharsfromlname = model.PatientLastName.Substring(0, 2);
            var stateabbr = model.State.Substring(0, 2);
            var date = DateTime.Now.Day.ToString("00");
            var month = DateTime.Now.Month.ToString("00");
            var totalRequests = _context.Requests.Where(r => r.CreatedDate.Date == DateTime.Now.Date).Count().ToString("0000");

            var userCheck = _context.Users.FirstOrDefault(u => u.Email == model.Email);

            var newRequest = new Request
            {
                RequestTypeId = 3,
                UserId = userCheck?.UserId,
                FirstName = model.FirstName,
                LastName = model.LastName,
                PhoneNumber = model.PhoneNumber,
                ContryCode = model.ContryCode,
                CreatedDate = DateTime.Now,
                Email = model.Email,
                PropertyName = model.PropertyName,
                Status = 1,
                ConfirmationNumber = (stateabbr + date + month + lasttwocharsfromlname + firsttwocharsfromfname + totalRequests).ToUpper(),
            };

            _context.Requests.Add(newRequest);
            _context.SaveChanges();

            var requestCheck = _context.Requests.OrderBy(e => e.RequestId).LastOrDefault(e => e.Email == model.Email);
            var newRequestClient = new RequestClient
            {
                
                RequestId = requestCheck.RequestId,
                FirstName = model.PatientFirstName,
                LastName = model.PatientLastName,
                PatientSymptoms = model.PatientSymptoms,
                PhoneNumber = model.PatientPhoneNumber,
                ContryCode = model.PatientContryCode,
                Email = model.PatientEmail,
                DateOfBirth = model.PatientDateOfBirth,
                IntDate = model.PatientDateOfBirth.Day,
                IntYear = model.PatientDateOfBirth.Year,
                StrMonth = model.PatientDateOfBirth.ToString("MMMM"),
                ZipCode = model.ZipCode,
                State = model.State,
                City = model.City,
                Street = model.Street,
                Address = model.Street + " ," + model.City + " ," + model.State + "-" + model.ZipCode,
                Location = model.PatientRoomNumber,
            };
            _context.RequestClients.Add(newRequestClient);
            _context.SaveChanges();

            return "Success";
        }

        public string BussinessTyperequest(BussinessRequestViewModel model)
        {
            var firsttwocharsfromfname = model.PatientFirstName.Substring(0, 2);
            var lasttwocharsfromlname = model.PatientLastName.Substring(0, 2);
            var stateabbr = model.PatientState.Substring(0, 2);
            var date = DateTime.Now.Day.ToString("00");
            var month = DateTime.Now.Month.ToString("00");
            var totalRequests = _context.Requests.Where(r => r.CreatedDate.Date == DateTime.Now.Date).Count().ToString("0000");

            var userCheck = _context.Users.FirstOrDefault(u => u.Email == model.Email);
            var newRequest = new Request
            {
                RequestTypeId = 4,
                UserId = userCheck?.UserId,
                FirstName = model.FirstName,
                LastName = model.LastName,
                PhoneNumber = model.PhoneNumber,
                ContryCode = model.ContryCode,
                CreatedDate = DateTime.Now,
                Email = model.Email,
                CaseNumber = model.CaseNumber,
                PropertyName = model.BusinessName,
                Status = 1,
                ConfirmationNumber = (stateabbr + date + month + lasttwocharsfromlname + firsttwocharsfromfname + totalRequests).ToUpper(),
            };
            _context.Requests.Add(newRequest);
            _context.SaveChanges();

            var requestCheck = _context.Requests.OrderBy(e => e.RequestId).LastOrDefault(e => e.Email == model.Email);
            var newRequestClient = new RequestClient
            {
                RequestId = requestCheck.RequestId,
                FirstName = model.PatientFirstName,
                LastName = model.PatientLastName,
                PatientSymptoms = model.PatientSymptoms,
                PhoneNumber = model.PatientPhoneNumber,
                ContryCode = model.PatientContryCode,
                Email = model.PatientEmail,
                DateOfBirth = model.PatientDateOfBirth,
                IntDate = model.PatientDateOfBirth.Day,
                IntYear = model.PatientDateOfBirth.Year,
                StrMonth = model.PatientDateOfBirth.ToString("MMMM"),
                ZipCode = model.PatientZipCode,
                State = model.PatientState,
                City = model.PatientCity,
                Street = model.PatientStreet,
                Address = model.PatientStreet + " ," + model.PatientCity + " ," + model.PatientState + "-" + model.PatientZipCode,
                Location = model.PatientRoomNumber,

            };

            _context.RequestClients.Add(newRequestClient);
            _context.SaveChanges();

            return "Success";
        }

        public string PatientRegisterPage(PatientRegisterViewModel model)
        {
            var aspnetuser = _context.AspNetUsers.Any(u => u.Email == model.Email);

            if (!aspnetuser)
            {
                var aspnetuser1 = new AspNetUser
                {
                    Email = model.Email,
                    UserName = "Abhshek Daraji",
                    PasswordHash = model?.PasswordHash,
                    CreatedDate = DateTime.Now,
                };
                _context.AspNetUsers.Add(aspnetuser1);
                _context.SaveChanges();

                var user = new User
                {
                    AspNetUserId = aspnetuser1.Id,
                    Email = model.Email,
                    FirstName = "Abhshek",
                    CreatedBy = "patient",
                    CreatedDate = DateTime.Now,
                };
                _context.Users.Add(user);
                _context.SaveChanges();
                return "Success";
            }
            else
            {
                return "userExits";
            }
        }

        public string PatientLoginPage(PatientLoginViewModel model, HttpContext httpContext)
        {
            AspNetUser aspNetUserFromDb = _context.AspNetUsers.FirstOrDefault(a => a.Email == model.Email);
            if (aspNetUserFromDb != null && aspNetUserFromDb.PasswordHash == model.PasswordHash)
            {
                var userFromDb = _context.Users.FirstOrDefault(b => b.AspNetUserId == aspNetUserFromDb.Id);

                CookieOptions cookieOption = new CookieOptions();
                cookieOption.Secure = true;
                cookieOption.Expires = DateTime.Now.AddMinutes(30);
                httpContext.Response.Cookies.Append("UserID", userFromDb.UserId.ToString(), cookieOption);
                httpContext.Response.Cookies.Append("EmailId", userFromDb.Email.ToString(), cookieOption);

                return "Success";
            }
            else if (aspNetUserFromDb == null)
            {

                return "InvalidEmail";
            }
            else
            {
                return "InvalidPssword";
            }

        }

        public object PatientDashboard(PatientDashboardViewModel model, HttpContext httpContext)
        {
            int userID = int.Parse(httpContext.Request.Cookies["UserID"]);

            PatientDashboardViewModel dashboardData = new PatientDashboardViewModel();
            dashboardData.User = _context.Users.FirstOrDefault(a => a.UserId == userID);
            dashboardData.RequestsData = _context.Requests.Where(b => b.UserId == userID).ToList();

            CookieOptions cookieOption = new CookieOptions();
            httpContext.Response.Cookies.Append("FirstName", dashboardData.User.FirstName, cookieOption);
            httpContext.Response.Cookies.Append("LastName", dashboardData.User.LastName, cookieOption);

            return dashboardData;

        }
       
        public object PatientUserProfile(PatientDashboardViewModel model, HttpContext httpContext)
        {
            int userID = int.Parse(httpContext.Request.Cookies["UserID"]);

            PatientDashboardViewModel dashboardData = new PatientDashboardViewModel();
            dashboardData.User = _context.Users.FirstOrDefault(a => a.UserId == userID);
            dashboardData.RequestsData = _context.Requests.Where(b => b.UserId == userID).ToList();

            CookieOptions cookieOption = new CookieOptions();
            httpContext.Response.Cookies.Append("FirstName", dashboardData.User.FirstName, cookieOption);
            httpContext.Response.Cookies.Append("LastName", dashboardData.User.LastName, cookieOption);

            return dashboardData;

        }

        public object UpdateUserProfile(PatientDashboardViewModel model, HttpContext httpContext)
        {
            var userid = int.Parse(httpContext.Request.Cookies["UserID"]);

            var user = _context.Users.FirstOrDefault(u => u.UserId == userid);

            if (user != null)
            {
                user.FirstName = model.ProfileEdited.FirstName;
                user.LastName = model.ProfileEdited.LastName;
                user.Mobile = model.ProfileEdited.Mobile;
                user.ContryCode = model.ProfileEdited.ContryCode;
                user.Street = model.ProfileEdited.Street;
                user.City = model.ProfileEdited.City;
                user.State = model.ProfileEdited.State;
                user.ZipCode = model.ProfileEdited.ZipCode;
                _context.Users.Update(user);
                _context.SaveChanges();
                return "Success";
            }
            return "Error";
        }

       
        public object PatientViewDocument( PatientViewDocumentViewModel model, int reqId, HttpContext httpContext)
        {
            int userID = int.Parse(httpContext.Request.Cookies["UserID"]);

            model.requestWiseFile = _context.RequestWiseFiles.Where(u => u.RequestId == reqId).ToList();
            model.request = _context.Requests.FirstOrDefault(u => u.RequestId == reqId);
            model.requestClient = _context.RequestClients.FirstOrDefault(u => u.RequestId == reqId);
            model.User = _context.Users.FirstOrDefault(a => a.UserId == userID);

            return model;
        }

        public string RequestMe(PatientCreateNewRequestViewModel model, HttpContext httpContext)
        {
            var firsttwocharsfromfname = model.PatientFirstName.Substring(0, 2);
            var lasttwocharsfromlname = model.PatientLastName.Substring(0, 2);
            var stateabbr = model.PatientState.Substring(0, 2);
            var date = DateTime.Now.Day.ToString("00");
            var month = DateTime.Now.Month.ToString("00");
            var totalRequests = _context.Requests.Where(r => r.CreatedDate.Date == DateTime.Now.Date).Count().ToString("0000");

            var userid = int.Parse(httpContext.Request.Cookies["UserID"]);
            var user = _context.Users.FirstOrDefault(u => u.UserId == userid);

            if (user != null)
            {
                var newRequest = new Request
                {
                    RequestTypeId = 1,
                    UserId = userid,
                    FirstName = model.PatientFirstName,
                    LastName = model.PatientLastName,
                    PhoneNumber = model.PatientPhoneNumber,
                    ContryCode = model.PatientContryCode,
                    CreatedDate = DateTime.Now,
                    Email = model.PatientEmail,
                    Status = 1,
                    ConfirmationNumber = (stateabbr + date + month + lasttwocharsfromlname + firsttwocharsfromfname + totalRequests).ToUpper(),

                };
                _context.Requests.Add(newRequest);
                _context.SaveChanges();

                var requestCheck = _context.Requests.OrderBy(a => a.RequestId).LastOrDefault(r => r.Email == model.PatientEmail);

                var newClientRequest = new RequestClient
                {
                    RequestId = requestCheck.RequestId,
                    FirstName = model.PatientFirstName,
                    LastName = model.PatientLastName,
                    PatientSymptoms=model.PatientSymptoms,
                    PhoneNumber = model.PatientPhoneNumber,
                    ContryCode = model.PatientContryCode,
                    Email = model.PatientEmail,
                    IntDate = model.PatientDateOfBirth.Day,
                    IntYear = model.PatientDateOfBirth.Year,
                    StrMonth = model.PatientDateOfBirth.ToString("MMMM"),
                    ZipCode = model.PatientZipCode,
                    State = model.PatientState,
                    City = model.PatientCity,
                    Street = model.PatientStreet,
                    Address =model.PatientStreet+ " ," + model.PatientCity + " ," + model.PatientState + "-" + model.PatientZipCode,
                    Location=model.PatientRoomNumber,

                };
                _context.RequestClients.Add(newClientRequest);
                _context.SaveChanges();

                //For File Store
                if (model.formFile != null && model.formFile.Count > 0)
                {
                    foreach (var file in model.formFile)
                    {
                        var fileId = _context.RequestWiseFiles.Count() + 1;
                        string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/UploadedFiles", ("(" + fileId.ToString() + ")") + file.FileName);

                        using (var stream = System.IO.File.Create(filePath))
                        {
                            file.CopyTo(stream);

                            var userCheck = _context.Requests.OrderBy(e => e.RequestId).LastOrDefault(e => e.Email == user.Email);
                            RequestWiseFile requestwiseFile = new RequestWiseFile();
                            requestwiseFile.RequestId = userCheck.RequestId;
                            requestwiseFile.FileName = file.FileName;
                            requestwiseFile.CreatedDate = DateTime.Now;
                            requestwiseFile.DocType = 1;

                            _context.RequestWiseFiles.Add(requestwiseFile);
                            _context.SaveChanges();
                        }
                    }
                }

               
            }
            return "Success";
        }

        public string RequestSomeOne(PatientCreateNewRequestViewModel model, HttpContext httpContext)
        {
            var firsttwocharsfromfname = model.PatientFirstName.Substring(0, 2);
            var lasttwocharsfromlname = model.PatientLastName.Substring(0, 2);
            var stateabbr = model.PatientState.Substring(0, 2);
            var date = DateTime.Now.Day.ToString("00");
            var month = DateTime.Now.Month.ToString("00");
            var totalRequests = _context.Requests.Where(r => r.CreatedDate.Date == DateTime.Now.Date).Count().ToString("0000");


            var userid = int.Parse(httpContext.Request.Cookies["UserID"]);
            var user = _context.Users.FirstOrDefault(u => u.UserId == userid);


            if (user != null)
            {
                var request = new Request
                {
                    UserId = userid,
                    RequestTypeId = 2,
                    FirstName = model.PatientFirstName,
                    LastName = model.PatientLastName,
                    PhoneNumber = model.PatientPhoneNumber,
                    ContryCode = model.PatientContryCode,
                    Email = model.PatientEmail,
                    CreatedDate = DateTime.Now,
                    RelationName = model.PatientRelationName,
                    Status = 1,
                    ConfirmationNumber = (stateabbr + date + month + lasttwocharsfromlname + firsttwocharsfromfname + totalRequests).ToUpper(),
                };
                _context.Requests.Add(request);
                _context.SaveChanges();

                var requestCheck = _context.Requests.OrderBy(a => a.RequestId).LastOrDefault(r => r.Email == model.PatientEmail);
                var requestclient = new RequestClient
                {
                    RequestId = requestCheck.RequestId,
                    FirstName = model.PatientFirstName,
                    LastName = model.PatientLastName,
                    PatientSymptoms=model.PatientSymptoms,
                    PhoneNumber = model.PatientPhoneNumber,
                    ContryCode = model.PatientContryCode,
                    Email = model.PatientEmail,
                    IntDate = model.PatientDateOfBirth.Day,
                    IntYear = model.PatientDateOfBirth.Year,
                    StrMonth = model.PatientDateOfBirth.ToString("MMMM"),
                    ZipCode = model.PatientZipCode,
                    State = model.PatientState,
                    City = model.PatientCity,
                    Street = model.PatientStreet,
                    Address = model.PatientStreet + " ," + model.PatientCity + " ," + model.PatientState + "-" + model.PatientZipCode,
                    Location=model.PatientRoomNumber,
                };
                _context.RequestClients.Add(requestclient);
                _context.SaveChanges();

                //For File Store
                if (model.formFile != null && model.formFile.Count > 0)
                {
                    foreach (var file in model.formFile)
                    { 
                        string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/UploadedFiles",  file.FileName);

                        using (var stream = System.IO.File.Create(filePath))
                        {
                            file.CopyTo(stream);

                            var userCheck = _context.Requests.OrderBy(e => e.RequestId).LastOrDefault(e => e.Email == user.Email);
                            RequestWiseFile requestwiseFile = new RequestWiseFile();
                            requestwiseFile.RequestId = userCheck.RequestId;
                            requestwiseFile.FileName = file.FileName;
                            requestwiseFile.CreatedDate = DateTime.Now;
                            requestwiseFile.DocType = 1;

                            _context.RequestWiseFiles.Add(requestwiseFile);
                            _context.SaveChanges();
                        }
                    }
                }

            }
            return "Success";
        }

        public string UploadDocuments(PatientViewDocumentViewModel model, int reqId)
        {
            if (model.formFile != null && model.formFile.Count > 0)
            {
                foreach (var file in model.formFile)
                {
                    string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/UploadedFiles", file.FileName);
                    using (var stream = System.IO.File.Create(filePath))
                    {
                        file.CopyTo(stream);
                        RequestWiseFile requestwiseFile = new RequestWiseFile();
                        requestwiseFile.RequestId = reqId;
                        requestwiseFile.FileName = file.FileName;
                        requestwiseFile.CreatedDate = DateTime.Now;
                        requestwiseFile.DocType = 1;

                        _context.RequestWiseFiles.Add(requestwiseFile);
                        _context.SaveChanges();
                    }
                }
            }
            return "Success";
        }


        public object PatientResetePassword(PatientResetePasswordViewModel model)
        {

            var userExist = _context.Users.FirstOrDefault(a => a.Email == model.Email);
            return userExist;

        }
        public string SendEmail(string email, string link)
        {
            try
            {
                var senderMail = "tatva.dotnet.girishsuthar@outlook.com";
                var senderPassword = "Girish@2299";

                SmtpClient smtpClient = new SmtpClient("smtp.office365.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential(senderMail, senderPassword),
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false
                };

                MailMessage mailMessage = new MailMessage
                {
                    From = new MailAddress(senderMail, "HalloDoc Password Setup"),
                    Subject = "Account Set up",
                    IsBodyHtml = true,
                    Body = "Click on " + "<a href=" + link + ">Account Set up</a>" + "For Update your passwrod",
                };

                mailMessage.To.Add(email);

                smtpClient.SendMailAsync(mailMessage);

                return "Success";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    
    
        public string PatientUpdatePassword(PatientUpdatePasswordViewModel model, HttpContext httpContext)
        {

            var email = httpContext.Request.Cookies["EmailId"];
            if (email == null)
            {
                return "emailNull";
            }
            var user = _context.AspNetUsers.FirstOrDefault(u => u.Email == email);
            if (model.PasswordHash == user.PasswordHash)
            {
                return "samePassword";
            }
            if (model.PasswordHash == model.ConfirmPassword)
            {
                user.PasswordHash = model.PasswordHash;
                _context.AspNetUsers.Update(user);
                _context.SaveChanges();
                 return "Success";
            }
            return  "error";
           
        }

    }
}