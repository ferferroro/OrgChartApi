using FluentValidation;

public class DepartmentValidator : AbstractValidator<Department>
{
    public DepartmentValidator()
    {
        RuleFor(p => p.Name).NotEmpty();
    }
}