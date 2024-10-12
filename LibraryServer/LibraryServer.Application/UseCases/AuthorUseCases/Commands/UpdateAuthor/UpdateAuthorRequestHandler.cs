using AutoMapper;
using LibraryServer.Domain.Entities;

namespace LibraryServer.Application.UseCases.AuthorUseCases.Commands;

internal class UpdateAuthorRequestHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : IRequestHandler<UpdateAuthorRequest>
{
    public async Task Handle(UpdateAuthorRequest request, CancellationToken cancellationToken = default)
    {
        var authorDb = await unitOfWork.AuthorRepository.GetByIdAsync(request.AuthorId);

        if (authorDb is null)
        {
            throw new NotFoundException($"No author with id={request.AuthorId}");
        }

        authorDb = mapper.Map<Author>(request.Author);

        //Validate(authorDb);

        await unitOfWork.AuthorRepository.UpdateAsync(authorDb);
        await unitOfWork.SaveAllAsync();
    }
}
