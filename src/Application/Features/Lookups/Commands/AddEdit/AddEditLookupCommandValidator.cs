namespace CleanArchitecture.Blazor.Application.Features.Lookups.Commands.AddEdit;

public class AddEditLookupCommandValidator : AbstractValidator<AddEditLookupCommand>
{
    public AddEditLookupCommandValidator()
    {
        RuleFor(v => v.Code).MaximumLength(255).NotEmpty();
        RuleFor(v => v.Name).MaximumLength(50).NotEmpty();
        RuleFor(v => v.NameBN).MaximumLength(255);

    }

}

