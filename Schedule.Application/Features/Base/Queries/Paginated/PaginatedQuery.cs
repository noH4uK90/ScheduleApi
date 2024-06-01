namespace Schedule.Application.Features.Base.Queries.Paginated;

public abstract record PaginatedQuery
{
    public required int Page { get; set; } = 1;
    public required int PageSize { get; set; } = 20;
}