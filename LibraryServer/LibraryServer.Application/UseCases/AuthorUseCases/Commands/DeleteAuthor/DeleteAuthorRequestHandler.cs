namespace LibraryServer.Application.UseCases.AuthorUseCases.Commands;

internal class DeleteAuthorRequestHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<DeleteAuthorRequest>
{
    public async Task Handle(DeleteAuthorRequest request, CancellationToken cancellationToken = default)
    {
        var author = await unitOfWork.AuthorRepository.GetByIdAsync(request.AuthorId);
        if (author is null)
        {
            throw new NotFoundException($"No author with id={request.AuthorId}");
        }

        await unitOfWork.AuthorRepository.DeleteAsync(author);
        await unitOfWork.SaveAllAsync();
    }
}
