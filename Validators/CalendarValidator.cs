using FluentValidation;

public class CalendarValidator : AbstractValidator<Calendar>
{
    public CalendarValidator()
    {
        RuleFor(p => p.Name).NotEmpty();
    }
}