
using HalloDocs.Entities.DataModels;
using HalloDocs.Entities.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HalloDocs.Repositories.Repository.Interface
{
    public interface IPatientRepository
    {

        public string PatientsTypeRequest(PatientRequestViewModel model);
        bool CheckEmailExists(string email);

        public string FamilyTypeRequest(FamilyFriendRequestViewModel model);

        public string ConciergeTypeRequest(ConciergeRequestViewModel model);

        public string BussinessTyperequest(BussinessRequestViewModel model);

        public string PatientRegisterPage(PatientRegisterViewModel model);

        public AspNetUser PatientLoginPage(PatientLoginViewModel model, HttpContext httpContext);

        public string PatienLogout(HttpContext httpContext);

        public object PatientDashboard(PatientDashboardViewModel model, HttpContext httpContext); 

        public object PatientUserProfile(PatientDashboardViewModel model, HttpContext httpContext); 

        public object UpdateUserProfile(PatientDashboardViewModel model, HttpContext httpContext);

        public object PatientViewDocument(PatientViewDocumentViewModel model, int reqId, HttpContext httpContext);

        public string UploadDocuments(PatientViewDocumentViewModel model, int reqId);

        public object GetRequestMe(PatientCreateNewRequestViewModel model, HttpContext httpContext);
        public string RequestMe(PatientCreateNewRequestViewModel model, HttpContext httpContext);

        public string RequestSomeOne(PatientCreateNewRequestViewModel model, HttpContext httpContext);

        public object PatientResetePassword(PatientResetePasswordViewModel model);

        public string SendEmail(string email, string link);

        public string PatientUpdatePassword(PatientUpdatePasswordViewModel model, HttpContext httpContext);

        //public object Download(int documentid);

        //public object DownloadAll(int reqId);



    }

}
