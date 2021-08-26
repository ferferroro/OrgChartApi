using FluentValidation;
using OrgChartApi.Models; 
using System.Linq;
using Microsoft.AspNetCore.Http;
using OrgChartApi.Models.DTOs.Requests;

public class EmployeeValidator : AbstractValidator<EmployeeRequest>
{
    public EmployeeValidator(OrgChartContext _context, IHttpContextAccessor _httpContext)
    {
        string requestMethod = _httpContext.HttpContext.Request.Method;

        // Validation rules for POST requests
        if (requestMethod == "POST") {
            RuleFor(p => p.FirstName).NotEmpty();
            RuleFor(p => p.LastName).NotEmpty();
            // RuleFor(p => p.Username)
            //     .NotEmpty()
            //     .Must(Username =>
            //         {
            //             return _context.Employee.FirstOrDefault(x => x.Username == Username) == null;
            //         })
            //         .WithMessage("'Username' already exists");
            // RuleFor(p => p.Password).NotEmpty();
            // RuleFor(p => p.ConfirmPassword)
            //     .NotEmpty()
            //     .Equal(p => p.Password)
            //     .WithMessage("'Confirm Password' does not match");
            
        }

        // Validation rules for GET Requests
        if (requestMethod == "GET") {

            When(payload => payload.FindById != 0, () => {
                RuleFor(payload => payload.FindById)
                    .Must(FindById =>
                        {
                            return _context.Employee.FirstOrDefault(x => x.Id == FindById) != null;
                        })
                    .WithMessage("'FindById' does not exists");
            });    

            When(payload => payload.SortOrder != null, () => {
                RuleFor(payload => payload.SortOrder)
                    .Must(SortOrder =>
                        {
                            return SortOrder == "asc" || SortOrder == "desc";
                        })
                    .WithMessage("'SortOrder' value must only be 'asc' or 'desc'");
            });        

            When(payload => payload.SortField != null, () => {
                RuleFor(payload => payload.SortField)
                    .Must(SortField =>
                        {
                            return SortField == "Id" || 
                                SortField == "FirstName" ||
                                SortField == "LastName";
                        })
                    .WithMessage("'SortField' value must only be 'Id,FirstName,LastName' or 'Username'");
            });    
        }     

        // Validation rules for PUT Requests
        if (requestMethod == "PUT") {

            // When(payload => payload.Username != null, () => {
            //     RuleFor(payload => payload.Username)
            //         .Must( (payload, Username) => { 
            //             var findEmployeeByUserName = _context.Employee.FirstOrDefault(x => x.Username == Username);

            //             if (findEmployeeByUserName == null) {
            //                 return true;
            //             }
            //             else {
            //                 if (findEmployeeByUserName.Id == payload.Id) {
            //                     return true;
            //                 }
            //                 else {
            //                     return false;
            //                 }
            //             }
            //         })
            //         .WithMessage("'Username' already taken");  
            // });   

        } 

        if ( HttpMethods.IsPost(requestMethod) || HttpMethods.IsPut(requestMethod)) {

            When(payload => payload.CalendarId != null, () => {
                RuleFor(payload => payload.CalendarId)
                    .Must(CalendarId =>
                        {
                            return _context.Calendar.FirstOrDefault(x => x.Id == CalendarId) != null;
                        })
                    .WithMessage("'CalendarId' does not exists");
            });  

            When(payload => payload.PayrollId != null, () => {
                RuleFor(payload => payload.PayrollId)
                    .Must(PayrollId =>
                        {
                            return _context.Payroll.FirstOrDefault(x => x.Id == PayrollId) != null;
                        })
                    .WithMessage("'PayrollId' does not exists");
            });  

            When(payload => payload.WorkStatusTemplateId != null, () => {
                RuleFor(payload => payload.WorkStatusTemplateId)
                    .Must(WorkStatusTemplateId =>
                        {
                            return _context.WorkStatusTemplate.FirstOrDefault(x => x.Id == WorkStatusTemplateId) != null;
                        })
                    .WithMessage("'WorkStatusTemplateId' does not exists");
            });  
        }

        
    }
}