using FluentValidation;
using OrgChartApi.Models; 
using System.Linq;

public class EmployeeValidator : AbstractValidator<Employee>
{
    public EmployeeValidator(OrgChartContext _context)
    {
        RuleFor(p => p.FirstName).NotEmpty();
        RuleFor(p => p.LastName).NotEmpty();
        RuleFor(p => p.Username)
            .NotEmpty()
            .Must(Username =>
                {
                    return _context.Employee.FirstOrDefault(x => x.Username == Username) == null;
                })
                .WithMessage("'Username' already exists");
        RuleFor(p => p.Password).NotEmpty();
        RuleFor(p => p.ConfirmPassword)
            .NotEmpty()
            .Equal(p => p.Password)
            .WithMessage("'Confirm Password' does not match");
        
    }
}