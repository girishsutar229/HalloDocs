using HalloDocs.Entities.DataContext;
using HalloDocs.Entities.DataModels;
using HalloDocs.Entities.ViewModels;
using HalloDocs.Repositories.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace HalloDocs.Repositories.Repository
{
    public class AdminRepository : IAdminRepository
    {
        private readonly ApplicationDbContext _context;
        public AdminRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public AspNetUser AdminLogin(AdminLoginViewModel model, HttpContext httpContext)
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
                httpContext.Response.Cookies.Append("FirstName", userFromDb.FirstName, cookieOption);
                httpContext.Response.Cookies.Append("LastName", userFromDb.LastName, cookieOption);

                return aspNetUserFromDb;
            }
            else if (aspNetUserFromDb == null)
            {
                return null;
            }
            else
            {
                return null;
            }

        }

        public string AdminLogOut(HttpContext httpContext)
        {

            httpContext.User = new ClaimsPrincipal(new ClaimsIdentity());

            // Delete the cookies
            httpContext.Response.Cookies.Delete("UserID");
            httpContext.Response.Cookies.Delete("EmailId");
            httpContext.Response.Cookies.Delete("FirstName");
            httpContext.Response.Cookies.Delete("LastName");
            httpContext.Response.Cookies.Delete("jwt");

            return "Logeout";
        }
       
        public List<AdminDashboardViewModel> GetNewRequest()
        {
            var data = (from request in _context.Requests
                        join client in _context.RequestClients on request.RequestId equals client.RequestId
                        where request.Status == 1
                        select new AdminDashboardViewModel
                        {
                            RequestId = request.RequestId,
                            RequestFirstName = request.FirstName,
                            RequestLastName = request.LastName,
                            RequestedDate = request.CreatedDate,
                            RequestTypeId = request.RequestTypeId,
                            RequestCountryCode = request.ContryCode,
                            RequestPhoneNumber = request.PhoneNumber,
                            ClientFirstName = client.FirstName,
                            ClientLastName = client.LastName,
                            ClientEmail = client.Email,
                            ClientCountryCode = client.ContryCode,
                            DateOfBirth = client.DateOfBirth,
                            Age = DateTime.Now.Year - client.DateOfBirth.Year,
                            ClientPhoneNumber = client.PhoneNumber,
                            ClientAddress= client.Address,
                            Notes = client.Notes
                        }).ToList();
          
            return data;
        }
     
        public List<AdminDashboardViewModel> GetPendingRequest()
        {
            var data = (from request in _context.Requests
                        join client in _context.RequestClients on request.RequestId equals client.RequestId
                        where request.Status == 2
                        select new AdminDashboardViewModel
                        {
                            RequestFirstName = request.FirstName,
                            RequestLastName = request.LastName,
                            RequestId = request.RequestId,
                            RequestedDate = request.CreatedDate,
                            RequestTypeId = request.RequestTypeId,
                            DateOfBirth = client.DateOfBirth,
                            Age = DateTime.Now.Year - client.DateOfBirth.Year,
                            RequestCountryCode = request.ContryCode,
                            RequestPhoneNumber = request.PhoneNumber,
                            ClientAddress = client.Address,
                            ClientCountryCode = client.ContryCode,
                            ClientPhoneNumber = client.PhoneNumber,
                            ClientFirstName = client.FirstName,
                            ClientLastName = client.LastName,
                            Notes = client.Notes,
                            ClientEmail = client.Email
                        }).ToList();

            return data;
        }

        public List<AdminDashboardViewModel> GetActiveRequest()
        {
            var data = (from request in _context.Requests
                        join client in _context.RequestClients on request.RequestId equals client.RequestId
                        where request.Status == 3
                        select new AdminDashboardViewModel
                        {
                            RequestFirstName = request.FirstName,
                            RequestLastName = request.LastName,
                            RequestId = request.RequestId,
                            RequestedDate = request.CreatedDate,
                            RequestTypeId = request.RequestTypeId,
                            DateOfBirth = client.DateOfBirth,
                            Age = DateTime.Now.Year - client.DateOfBirth.Year,
                            RequestCountryCode = request.ContryCode,
                            RequestPhoneNumber = request.PhoneNumber,
                            ClientAddress = client.Address,
                            ClientCountryCode = client.ContryCode,
                            ClientPhoneNumber = client.PhoneNumber,
                            ClientFirstName = client.FirstName,
                            ClientLastName = client.LastName,
                            Notes = client.Notes,
                            ClientEmail = client.Email
                        }).ToList();

            return data;
        }


        public List<AdminDashboardViewModel> GetConcludeRequest()
        {
            var data = (from request in _context.Requests
                        join client in _context.RequestClients on request.RequestId equals client.RequestId
                        where request.Status == 4
                        select new AdminDashboardViewModel
                        {
                            RequestFirstName = request.FirstName,
                            RequestLastName = request.LastName,
                            RequestId = request.RequestId,
                            RequestedDate = request.CreatedDate,
                            RequestTypeId = request.RequestTypeId,
                            DateOfBirth = client.DateOfBirth,
                            Age = DateTime.Now.Year - client.DateOfBirth.Year,
                            RequestCountryCode = request.ContryCode,
                            RequestPhoneNumber = request.PhoneNumber,
                            ClientAddress = client.Address,
                            ClientCountryCode = client.ContryCode,
                            ClientPhoneNumber = client.PhoneNumber,
                            ClientFirstName = client.FirstName,
                            ClientLastName = client.LastName,
                            Notes = client.Notes,
                            ClientEmail = client.Email
                        }).ToList();

            return data;
        }

        public List<AdminDashboardViewModel> GetToCloseRequest()
        {
            var data = (from request in _context.Requests
                        join client in _context.RequestClients on request.RequestId equals client.RequestId
                        where request.Status == 5
                        select new AdminDashboardViewModel
                        {
                            RequestFirstName = request.FirstName,
                            RequestLastName = request.LastName,
                            RequestId = request.RequestId,
                            RequestedDate = request.CreatedDate,
                            RequestTypeId = request.RequestTypeId,
                            DateOfBirth = client.DateOfBirth,
                            Age = DateTime.Now.Year - client.DateOfBirth.Year,
                            RequestCountryCode = request.ContryCode,
                            RequestPhoneNumber = request.PhoneNumber,
                            ClientAddress = client.Address,
                            ClientCountryCode = client.ContryCode,
                            ClientPhoneNumber = client.PhoneNumber,
                            ClientFirstName = client.FirstName,
                            ClientLastName = client.LastName,
                            Notes = client.Notes,
                            ClientEmail = client.Email
                        }).ToList();

            return data;
        }

        public List<AdminDashboardViewModel> GetUnPaidRequest()
        {
            var data = (from request in _context.Requests
                        join client in _context.RequestClients on request.RequestId equals client.RequestId
                        where request.Status == 6
                        select new AdminDashboardViewModel
                        {
                            RequestFirstName = request.FirstName,
                            RequestLastName = request.LastName,
                            RequestId = request.RequestId,
                            RequestedDate = request.CreatedDate,
                            RequestTypeId = request.RequestTypeId,
                            DateOfBirth = client.DateOfBirth,
                            Age = DateTime.Now.Year - client.DateOfBirth.Year,
                            RequestCountryCode = request.ContryCode,
                            RequestPhoneNumber = request.PhoneNumber,
                            ClientAddress = client.Address,
                            ClientCountryCode = client.ContryCode,
                            ClientPhoneNumber = client.PhoneNumber,
                            ClientFirstName = client.FirstName,
                            ClientLastName = client.LastName,
                            Notes = client.Notes,
                            ClientEmail = client.Email
                        }).ToList();

            return data;
        }



        public object GetViewCase(ViewCaseViewModel model, int reqId)
        {
            ViewCaseViewModel viewCaseData = new ViewCaseViewModel();
            viewCaseData.requestclient = _context.RequestClients.FirstOrDefault(a => a.RequestId == reqId);
            var requestData = _context.Requests.FirstOrDefault(a => a.RequestId == viewCaseData.requestclient.RequestId);
            viewCaseData.requestType = requestData.RequestTypeId;
            viewCaseData.status = requestData.Status;
            viewCaseData.ConfirmationNumber = requestData.ConfirmationNumber;

            return viewCaseData;
        }

  

    }
}
