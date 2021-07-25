using FluentValidation;

public class EmployeeValidator : AbstractValidator<Employee>
{
    public EmployeeValidator()
    {
        RuleFor(p => p.Name).NotEmpty();
    }
}