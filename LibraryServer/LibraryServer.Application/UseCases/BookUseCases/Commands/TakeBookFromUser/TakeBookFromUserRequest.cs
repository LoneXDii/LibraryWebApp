namespace LibraryServer.Application.UseCases.BookUseCases.Commands;

public sealed record TakeBookFromUserRequest(GiveOrTakeBookDTO TakeBookObj) : IRequest { }