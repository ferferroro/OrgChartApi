using System;
using System.Linq;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using OrgChartApi.Models;
using OrgChartApi.Models.DTOs.Requests;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

public class EntityMembersValidator : AbstractValidator<EntityMembersRequest>
{
    public EntityMembersValidator(OrgChartContext _context, IHttpContextAccessor _httpContext)
    {
        string requestMethod = _httpContext.HttpContext.Request.Method;

        

        if ( HttpMethods.IsPost(requestMethod) || HttpMethods.IsPut(requestMethod)) {

            RuleFor(payload => payload.EmployeeId).NotEmpty();

            When(payload => payload.EmployeeId != null, () => {
                RuleFor(payload => payload.EmployeeId)
                    .Must(EmployeeId =>
                        {
                            return _context.Employee.FirstOrDefault(x => x.Id == EmployeeId) != null;
                        })
                    .WithMessage("'EmployeeId' does not exists");
            });  

            When(payload => payload.CompanyId != null, () => {
                RuleFor(payload => payload.CompanyId)
                    .Must(CompanyId =>
                        {
                            return _context.Company.FirstOrDefault(x => x.Id == CompanyId) != null;
                        })
                    .WithMessage("'CompanyId' does not exists");
            });  

            When(payload => payload.DepartmentId != null, () => {
                RuleFor(payload => payload.DepartmentId)
                    .Must(DepartmentId =>
                        {
                            return _context.Department.FirstOrDefault(x => x.Id == DepartmentId) != null;
                        })
                    .WithMessage("'DepartmentId' does not exists");
            });  

            When(payload => payload.TeamId != null, () => {
                RuleFor(payload => payload.TeamId)
                    .Must(TeamId =>
                        {
                            return _context.Team.FirstOrDefault(x => x.Id == TeamId) != null;
                        })
                    .WithMessage("'TeamId' does not exists");
            });  

            When(payload => payload.SubTeamId != null, () => {
                RuleFor(payload => payload.SubTeamId)
                    .Must(SubTeamId =>
                        {
                            return _context.SubTeam.FirstOrDefault(x => x.Id == SubTeamId) != null;
                        })
                    .WithMessage("'SubTeamId' does not exists");
            });  


            When(payload => payload.EmployeeId != null, () => {
                RuleFor(payload => payload.EmployeeId)
                    .Must( (payload, EmployeeId) => { 

                        if ( 
                            payload.CompanyId == null &&
                            payload.DepartmentId == null   &&
                            payload.TeamId == null          &&
                            payload.SubTeamId == null
                        ) {
                            return false;
                        }
                        else {
                            return true;
                        }
                    })
                    .WithMessage("'EmployeeId' Please enter atleast one Entity Id");  
            });  
            
        }

    }

}