using System.Linq.Expressions;

namespace LibraryServer.Application.UseCases.BookUseCases.Queries;

public sealed record GetFirstOrDefaultBookRequest(Expression<Func<BookDTO, bool>> Filter)
    : IRequest<BookDTO>
{ }