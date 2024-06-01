using FluentValidation;
using Schedule.Application.Features.Base.Queries.Paginated;

namespace Schedule.Application.Features.Accounts.Queries.GetList;

public sealed class GetAccountListQueryValidator : AbstractValidator<GetAccountListQuery>
{
    public GetAccountListQueryValidator()
    {
        RuleFor(query => query)
            .SetValidator(new PaginatedQueryValidator());
    }
}