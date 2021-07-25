using FluentValidation;
using OrgChartApi.Models; 
using System.Linq;
using System;

public class CalendarEventValidator : AbstractValidator<CalendarEvent>
{
    public CalendarEventValidator(OrgChartContext _context)
    {
        RuleFor(p => p.Name).NotEmpty();
        RuleFor(p => p.Type).NotEmpty();
        RuleFor(p => p.DateFrom).NotEmpty();
        RuleFor(p => p.DateTo).NotEmpty()
            .GreaterThanOrEqualTo(a => a.DateFrom.Date)
            .WithMessage("'Date To' must not be greater than or equal to 'Date From'")
            ;
        RuleFor(p => p.CalendarId)
            .Must(CalendarId =>
                {
                    return _context.Calendar.FirstOrDefault(id => id.Id == CalendarId) != null;
                })
                .WithMessage("'CalendarId' does not exists");
    }
}