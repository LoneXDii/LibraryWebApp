using AutoMapper;

namespace LibraryServer.Application.UseCases.AuthorUseCases.Queries;

internal class GetAuthorByIdRequestHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : IRequestHandler<GetAuthorByIdRequest, AuthorDTO>
{
    public async Task<AuthorDTO> Handle(GetAuthorByIdRequest request, CancellationToken cancellationToken = default)
    {
        var author = await unitOfWork.AuthorRepository.GetByIdAsync(request.AuthorId);

        if (author is null)
        {
            throw new NotFoundException($"No author with id={request.AuthorId}");
        }

        var authorDto = mapper.Map<AuthorDTO>(author);
        return authorDto;
    }
}
