using AutoMapper;

namespace LibraryServer.Application.UseCases.AuthorUseCases.Commands;

internal class AddAuthorRequestHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : IRequestHandler<AddAuthorRequest, AuthorDTO>
{
    public async Task<AuthorDTO> Handle(AddAuthorRequest request, CancellationToken cancellationToken = default)
    {
        var authorDb = mapper.Map<Author>(request.Author);

        authorDb = await unitOfWork.AuthorRepository.AddAsync(authorDb);
        await unitOfWork.SaveAllAsync();

        var author = mapper.Map<AuthorDTO>(authorDb);
        return author;
    }
}
