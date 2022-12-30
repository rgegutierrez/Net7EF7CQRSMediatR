using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using MediatrExample.ApplicationCore.Common.Helpers;
using MediatrExample.ApplicationCore.Domain;
using MediatrExample.ApplicationCore.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace MediatrExample.ApplicationCore.Features.ProductoQuimicoFeatures.Queries;

public class GetListProductoQuimicoQuery : IRequest<List<GetListProductoQuimicoQueryResponse>>
{

}

public class GetListProductoQuimicoQueryHandler : IRequestHandler<GetListProductoQuimicoQuery, List<GetListProductoQuimicoQueryResponse>>
{
    private readonly MyAppDbContext _context;
    private readonly IMapper _mapper;

    public GetListProductoQuimicoQueryHandler(MyAppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public Task<List<GetListProductoQuimicoQueryResponse>> Handle(GetListProductoQuimicoQuery request, CancellationToken cancellationToken)
    {
        string sql = "EXEC [trzreceta].[SyncProductoQuimico]";
        _context.ProductosQuimicos.FromSqlRaw<ProductoQuimico>(sql).ToList();

        return _context.ProductosQuimicos
            .AsNoTracking()
            .ProjectTo<GetListProductoQuimicoQueryResponse>(_mapper.ConfigurationProvider)
            .ToListAsync();
    }
}

public class GetListProductoQuimicoQueryResponse
{
    public string ProductoQuimicoId { get; set; } = default!;
    public string CodigoSap { get; set; } = default!;
    public string NombreVariable { get; set; } = default!;
    public string UnidadMedida { get; set; } = default!;
    public decimal ValorMinimo { get; set; }
    public decimal ValorMaximo { get; set; }
    public string Funcion { get; set; } = default!;
    public bool Certificacion { get; set; }
    public bool Obligatoria { get; set; }
    public bool Estado { get; set; }
    public string StrValorMinimo { get; set; } = default!;
    public string StrValorMaximo { get; set; } = default!;
}

public class GetListProductoQuimicoQueryProfile : Profile
{
    public GetListProductoQuimicoQueryProfile() =>
        CreateMap<ProductoQuimico, GetListProductoQuimicoQueryResponse>()
            .ForMember(dest =>
                dest.StrValorMinimo,
                opt => opt.MapFrom(mf => $"{mf.ValorMinimo}{mf.UnidadMedida}"))
            .ForMember(dest =>
                dest.StrValorMaximo,
                opt => opt.MapFrom(mf => $"{mf.ValorMaximo}{mf.UnidadMedida}"))
            .ForMember(dest =>
                dest.ProductoQuimicoId,
                opt => opt.MapFrom(mf => mf.ProductoQuimicoId.ToHashId()));

}
