using System;
using System.Linq;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using OrgChartApi.Models;
using OrgChartApi.Models.DTOs.Requests;

public class PayrollValidator : AbstractValidator<PayrollRequest>
{
    public PayrollValidator(OrgChartContext _context, IHttpContextAccessor _httpContext)
    {
        

        string requestMethod = _httpContext.HttpContext.Request.Method;

        // Validation rules for POST requests
        if ( HttpMethods.IsPost(requestMethod)) {
            RuleFor(p => p.Name).NotEmpty();
            RuleFor(p => p.CutOffType).NotEmpty()
                .Must((CutOffType) => IsCutOffTypeValid(CutOffType))
                .WithMessage("CutOffType must only be 'Monthly' or 'Semi-monthly'");
            
            When(payload => payload.CutOffType == "Monthly", () => {
                RuleFor(p => p.CutOffDay1).NotEmpty()
                    .Must((CutOffDay1) => IsAValidDate(CutOffDay1))
                    .WithMessage("CutOffDay1 must be a valid Date");
                
                RuleFor(p => p.CutOffDay2).NotEmpty()
                    .Must((CutOffDay2) => IsAValidDate(CutOffDay2))
                    .WithMessage("CutOffDay2 must be a valid Date");

                RuleFor(p => p.PayrollDay1).NotEmpty()
                    .Must((PayrollDay1) => IsAValidDate(PayrollDay1))
                    .WithMessage("PayrollDay1 must be a valid Date");
            });

            When(payload => payload.CutOffType == "Semi-monthly", () => {
                RuleFor(p => p.CutOffDay1).NotEmpty()
                    .Must((CutOffDay1) => IsAValidDate(CutOffDay1))
                    .WithMessage("CutOffDay1 must be a valid Date");
                
                RuleFor(p => p.CutOffDay2).NotEmpty()
                    .Must((CutOffDay2) => IsAValidDate(CutOffDay2))
                    .WithMessage("CutOffDay2 must be a valid Date");

                RuleFor(p => p.CutOffDay3).NotEmpty()
                    .Must((CutOffDay3) => IsAValidDate(CutOffDay3))
                    .WithMessage("CutOffDay3 must be a valid Date");
                
                RuleFor(p => p.CutOffDay4).NotEmpty()
                    .Must((CutOffDay4) => IsAValidDate(CutOffDay4))
                    .WithMessage("CutOffDay4 must be a valid Date");

                RuleFor(p => p.PayrollDay1).NotEmpty()
                    .Must((PayrollDay1) => IsAValidDate(PayrollDay1))
                    .WithMessage("PayrollDay1 must be a valid Date");
                
                RuleFor(p => p.PayrollDay2).NotEmpty()
                    .Must((PayrollDay2) => IsAValidDate(PayrollDay2))
                    .WithMessage("PayrollDay2 must be a valid Date");
            });
            
            
            // soon we add checking if child records are available
            
        }

        // Validation rules for POST requests
        if ( HttpMethods.IsGet(requestMethod)) {

            When(payload => payload.FindById != 0, () => {
                RuleFor(payload => payload.FindById)
                    .Must(FindById =>
                        {
                            return _context.Payroll.FirstOrDefault(x => x.Id == FindById) != null;
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

    private bool IsCutOffTypeValid(string inputCutOffType)
    {

        return (
            inputCutOffType == "Monthly" ||
            inputCutOffType == "Semi-monthly"
            
        );
    }

    private bool IsAValidDate(DateTime date)
    {
        return !date.Equals(default(DateTime));
    }
}