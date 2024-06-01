using FluentValidation;

namespace Schedule.Application.Features.Base.Queries.Paginated;

public sealed class PaginatedQueryValidator : AbstractValidator<PaginatedQuery>
{
    public PaginatedQueryValidator()
    {
        RuleFor(query => query.Page)
            .GreaterThan(0);
        RuleFor(query => query.PageSize)
            .InclusiveBetween(1, 100);
    }
}