using FastEndpoints;
using FluentValidation;

namespace LeadLeague.Common.Vms;

public record IdVm(Guid Id);

public abstract class IdVmValidator<T> : Validator<T> where T : IdVm
{
    public IdVmValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Id is required"); ;
    }
}
