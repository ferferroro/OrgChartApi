using FluentValidation;

public class PayrollValidator : AbstractValidator<Payroll>
{
    public PayrollValidator()
    {
        RuleFor(p => p.Name).NotEmpty();
        RuleFor(p => p.CutOffType).NotEmpty()
            .Must((CutOffType) => IsCutOffTypeValid(CutOffType))
            .WithMessage("CutOffType must only be 'Monthly' or 'Semi-monthly'");
    }

    private bool IsCutOffTypeValid(string inputCutOffType)
    {

        return (
            inputCutOffType == "Monthly" ||
            inputCutOffType == "Semi-monthly"
            
        );
    }
}