using HalloDocs.Entities.DataContext;
using HalloDocs.Entities.DataModels;
using HalloDocs.Entities.ViewModels;
using HalloDocs.Repositories.Repository.Interface;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
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
                            ClientAddress = client.City +","+ client.Street +","+ client.State,
                            ClientCountryCode = client.ContryCode,
                            ClientPhoneNumber = client.PhoneNumber,
                            ClientFirstName = client.FirstName,
                            ClientLastName = client.LastName,
                            Notes = client.Notes,
                            ClientEmail = client.Email
                        }).ToList();

            return data;
        }
        public AdminDashboardViewModel ViewCase(int reqId)
        {
            var requestClient = _context.RequestClients.FirstOrDefault(a => a.RequestId == reqId);
            var request = _context.Requests.FirstOrDefault(r => r.RequestId == reqId);
            var data = new AdminDashboardViewModel
            {
                RequestId = request.RequestId,
                ConfirmationNumber = request.ConfirmationNumber,
                ClientFirstName = requestClient.FirstName,
                ClientLastName = requestClient.LastName,
                DateOfBirth = requestClient.DateOfBirth,
                ClientPhoneNumber = requestClient.PhoneNumber,
                ClientCountryCode = requestClient.ContryCode,
                ClientEmail = requestClient.Email,
                ClientAddress = requestClient.Address,
                Status = request.Status,
                RequestTypeId = request.RequestTypeId,
            };

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
                            ClientAddress = client.City + client.Street + client.State,
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
                            ClientAddress = client.City + client.Street + client.State,
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
                            ClientAddress = client.City + client.Street + client.State,
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
                            ClientAddress = client.City + client.Street + client.State,
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
                            ClientAddress = client.City + client.Street + client.State,
                            ClientCountryCode = client.ContryCode,
                            ClientPhoneNumber = client.PhoneNumber,
                            ClientFirstName = client.FirstName,
                            ClientLastName = client.LastName,
                            Notes = client.Notes,
                            ClientEmail = client.Email
                        }).ToList();

            return data;
        }

      

    
    }
}
