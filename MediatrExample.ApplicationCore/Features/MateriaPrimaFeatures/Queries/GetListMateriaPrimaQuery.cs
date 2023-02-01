using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using MediatrExample.ApplicationCore.Common.Helpers;
using MediatrExample.ApplicationCore.Domain;
using MediatrExample.ApplicationCore.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace MediatrExample.ApplicationCore.Features.MateriaPrimaFeatures.Queries;

public class GetListMateriaPrimaQuery : IRequest<List<GetListMateriaPrimaQueryResponse>>
{

}

public class GetListMateriaPrimaQueryHandler : IRequestHandler<GetListMateriaPrimaQuery, List<GetListMateriaPrimaQueryResponse>>
{
    private readonly MyAppDbContext _context;
    private readonly IMapper _mapper;

    public GetListMateriaPrimaQueryHandler(MyAppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public Task<List<GetListMateriaPrimaQueryResponse>> Handle(GetListMateriaPrimaQuery request, CancellationToken cancellationToken)
    {
        string sql = "EXEC [trzreceta].[SyncMateriaPrima]";
        _context.MateriasPrimas.FromSqlRaw<MateriaPrima>(sql).ToList();

        return _context.MateriasPrimas
            .AsNoTracking()
            .ProjectTo<GetListMateriaPrimaQueryResponse>(_mapper.ConfigurationProvider)
            .ToListAsync();
    }
}

public class GetListMateriaPrimaQueryResponse
{
    public string MateriaPrimaId { get; set; } = default!;
    public string CodigoSap { get; set; } = default!;
    public string NombreVariable { get; set; } = default!;
    public string UnidadMedida { get; set; } = default!;
    public decimal ValorMinimo { get; set; }
    public decimal ValorMaximo { get; set; }
    public bool Obligatoria { get; set; }
    public bool Estado { get; set; }
    public string ValorMinimoStr { get; set; } = default!;
    public string ValorMaximoStr { get; set; } = default!;
}

public class GetListMateriaPrimaQueryProfile : Profile
{
    public GetListMateriaPrimaQueryProfile() =>
        CreateMap<MateriaPrima, GetListMateriaPrimaQueryResponse>()
            .ForMember(dest =>
                dest.ValorMinimoStr,
                opt => opt.MapFrom(mf => mf.ValorMinimo.FromDotToComma()))
            .ForMember(dest =>
                dest.ValorMaximoStr,
                opt => opt.MapFrom(mf => mf.ValorMaximo.FromDotToComma()))
            .ForMember(dest =>
                dest.MateriaPrimaId,
                opt => opt.MapFrom(mf => mf.MateriaPrimaId.ToHashId()));

}
