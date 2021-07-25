using FluentValidation;

public class CompanyValidator : AbstractValidator<Company>
{
    public CompanyValidator()
    {
        RuleFor(p => p.Name).NotEmpty();
    }
}