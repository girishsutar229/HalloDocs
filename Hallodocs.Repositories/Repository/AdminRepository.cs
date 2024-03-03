using HalloDocs.Entities.DataContext;
using HalloDocs.Entities.DataModels;
using HalloDocs.Entities.ViewModels;
using HalloDocs.Repositories.Repository.Interface;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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

        //public string AdminLogin(AdminLoginViewModel model)
        //{
        //    var adminemail = _context.Admins.Any(a => a.Email == model.Email);

        //    if (!adminemail)
        //    {
        //        var admin = new Admin
        //        {
        //            FirstName = "Girish Gajjar",
        //            AspNetUserId = 1,
        //            Email = "girish@gmail.com",
        //            CreatedDate = DateTime.Now,

        //        };
        //        _context.Admins.Add(admin);
        //        _context.SaveChanges();
        //        return "Success";
        //    }
        //    else
        //    {
        //        return "userExits";
        //    }
        //}

        public List<AdminDashboardViewModel> GetNewRequest()
        {
            var data = (from request in _context.Requests
                        join client in _context.RequestClients on request.RequestId equals client.RequestId
                        where request.Status == 1
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
                            ClientAddress= client.Address,
                            ClientCountryCode = client.ContryCode,
                            ClientPhoneNumber = client.PhoneNumber,
                            ClientFirstName = client.FirstName,
                            ClientLastName = client.LastName,
                            Notes = client.Notes,
                            ClientEmail = client.Email
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
