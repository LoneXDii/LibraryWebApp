namespace LibraryServer.Application.UseCases.BookUseCases.Queries;

public sealed record ListBooksWithPaginationRequest(string? GenreNormalizedName,
    int PageNo = 1, int PageSize = 9) : IRequest<PaginatedListModel<BookDTO>>
{ }