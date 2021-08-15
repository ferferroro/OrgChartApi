using FluentValidation;
using OrgChartApi.Models.DTOs.Requests;
using OrgChartApi.Models; 
using System.Linq;
using Microsoft.AspNetCore.Http;

public class CompanyValidator : AbstractValidator<CompanyRequest>
{
    
    public CompanyValidator(OrgChartContext _context, IHttpContextAccessor _httpContext)
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


    }
}