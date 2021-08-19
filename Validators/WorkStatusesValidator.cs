using System;
using System.Linq;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using OrgChartApi.Models;
using OrgChartApi.Models.DTOs.Requests;

public class WorkStatusesValidator : AbstractValidator<WorkStatusesRequest>
{
    public WorkStatusesValidator(OrgChartContext _context, IHttpContextAccessor _httpContext)
    {
        string requestMethod = _httpContext.HttpContext.Request.Method;

        // Validation rules for POST requests
        if ( HttpMethods.IsPost(requestMethod) || HttpMethods.IsPut(requestMethod)) {
            RuleFor(p => p.WorkStatusTemplateId).NotEmpty();

            // When(p => p.WorkStatusTemplateId != 0, () => {
            //     RuleFor(p => p.WorkStatusTemplateId)
            //         .Must( (p, WorkStatusTemplateId) => { 
            //             var findWorkStatusTemplate = _context.Employee.FirstOrDefault(x => x.Id == WorkStatusTemplateId);

            //             if (findWorkStatusTemplate == null) {
            //                 return false;
            //             }
            //             else {
            //                 if (findWorkStatusTemplate.Id == p.Id) {
            //                     return false;
            //                 }
            //                 else {
            //                     return true;
            //                 }
            //             }
            //         })
            //         .WithMessage("'WorkStatusTemplateId' does not exists");  
            // });   



            // RuleFor(p => p.WorkStatusId).NotEmpty();

            // When(p => p.WorkStatusId != 0, () => {
            //     RuleFor(p => p.WorkStatusId)
            //         .Must( (p, WorkStatusId) => { 
            //             var findWorkStatus = _context.Employee.FirstOrDefault(x => x.Id == WorkStatusId);

            //             if (findWorkStatus == null) {
            //                 return false;
            //             }
            //             else {
            //                 if (findWorkStatus.Id == p.Id) {
            //                     return false;
            //                 }
            //                 else {
            //                     return true;
            //                 }
            //             }
            //         })
            //         .WithMessage("'WorkStatusId' does not exists");  
            // });   
        
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

    }

}