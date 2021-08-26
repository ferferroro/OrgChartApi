using System;
using System.Linq;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using OrgChartApi.Models;
using OrgChartApi.Models.DTOs.Requests;

public class TeamValidator : AbstractValidator<TeamRequest>
{
    public TeamValidator(OrgChartContext _context, IHttpContextAccessor _httpContext)
    {
        string requestMethod = _httpContext.HttpContext.Request.Method;

        // Validation rules for POST requests
        if ( HttpMethods.IsPost(requestMethod)) {
            RuleFor(p => p.Name).NotEmpty();
        
            // soon we add checking if child records are available
        }

        // Validation rules for POST requests
        if ( HttpMethods.IsGet(requestMethod)) {

            When(payload => payload.FindById != 0, () => {
                RuleFor(payload => payload.FindById)
                    .Must(FindById =>
                        {
                            return _context.Company.FirstOrDefault(x => x.Id == FindById) != null;
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
                                SortField == "Name";
                        })
                    .WithMessage("'SortField' value must only be 'Id' or 'Name'");
            }); 
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

            When(payload => payload.EmployeeId != null, () => {
                RuleFor(payload => payload.EmployeeId)
                    .Must(EmployeeId =>
                        {
                            return _context.Employee.FirstOrDefault(x => x.Id == EmployeeId) != null;
                        })
                    .WithMessage("'EmployeeId' does not exists");
            });  
        }

    }

}