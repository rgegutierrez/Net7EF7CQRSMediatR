using AutoMapper;
using FluentValidation;
using MediatR;
using MediatrExample.ApplicationCore.Common.Helpers;
using MediatrExample.ApplicationCore.Domain;
using MediatrExample.ApplicationCore.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace MediatrExample.ApplicationCore.Features.MateriaPrimaFeatures.Commands;
public class SyncMateriaPrimaCommand : IRequest
{

}

public class SyncMateriaPrimaCommandHandler : IRequestHandler<SyncMateriaPrimaCommand>
{
    private readonly MyAppDbContext _context;
    private readonly IMapper _mapper;

    public SyncMateriaPrimaCommandHandler(MyAppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(SyncMateriaPrimaCommand request, CancellationToken cancellationToken)
    {
        List<MateriaPrima> list;
        string sql = "EXEC [trzreceta].[SyncMateriaPrima]";
        list = _context.MateriasPrimas.FromSqlRaw<MateriaPrima>(sql).ToList();

        await _context.SaveChangesAsync();

        return Unit.Value;
    }
}
public class SyncMateriaPrimaCommandMapper : Profile
{
    public SyncMateriaPrimaCommandMapper() =>
        CreateMap<SyncMateriaPrimaCommand, MateriaPrima>();
}

public class SyncMateriaPrimaValidator : AbstractValidator<SyncMateriaPrimaCommand>
{
    public SyncMateriaPrimaValidator()
    {

    }
}
