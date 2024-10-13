using AutoMapper;
using AutoMapper.Extensions.ExpressionMapping;
using LibraryServer.Application.Mapping;
using LibraryServer.Domain.Data;
using LibraryServer.Domain.Abstactions;
using LibraryServer.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Configuration;
using LibraryServer.Domain.BlobStorage;
using Moq;

namespace LibraryServer.Tests.Common;

public class TestCommandBase : IDisposable
{
    protected readonly AppDbContext _dbContext;
    protected readonly IMapper _mapper;
    protected readonly IUnitOfWork _unitOfWork;
    protected readonly IConfiguration _cfg;
    protected readonly IBlobService _blobService;

    public TestCommandBase()
    {
        _dbContext = LibraryContextFactory.Create();
        var authorRepository = new Repository<Author>(_dbContext);
        var genreRepository = new Repository<Genre>(_dbContext);
        var bookRepository = new Repository<Book>(_dbContext);
        var takenBookRepository = new Repository<TakenBook>(_dbContext);
        _unitOfWork = new UnitOfWork(_dbContext, bookRepository, authorRepository, genreRepository, takenBookRepository);

        var mapperCfg = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new AppMappingProfile());
            cfg.AddExpressionMapping();
        });

        _mapper = new Mapper(mapperCfg);
        _cfg = Mock.Of<IConfiguration>();
        _blobService = Mock.Of<IBlobService>();
    }

    public void Dispose()
    {
        LibraryContextFactory.Destroy(_dbContext);
    }
}
