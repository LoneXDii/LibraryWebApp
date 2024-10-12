namespace LibraryServer.Application.UseCases.BookUseCases.Commands;

public sealed record GiveBookToUserRequest(GiveOrTakeBookDTO GiveBookObj) : IRequest { }