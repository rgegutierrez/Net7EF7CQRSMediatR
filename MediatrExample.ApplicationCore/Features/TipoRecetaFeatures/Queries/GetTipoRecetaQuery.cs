using AutoMapper;
using MediatR;
using MediatrExample.ApplicationCore.Common.Exceptions;
using MediatrExample.ApplicationCore.Common.Helpers;
using MediatrExample.ApplicationCore.Domain;
using MediatrExample.ApplicationCore.Infrastructure.Persistence;


namespace MediatrExample.ApplicationCore.Features.TipoRecetaFeatures.Queries;

public class GetTipoRecetaQuery : IRequest<GetTipoRecetaQueryResponse>
{
    public string TipoRecetaId { get; set; }
}

public class GetTipoRecetaQueryHandler : IRequestHandler<GetTipoRecetaQuery, GetTipoRecetaQueryResponse>
{
    private readonly MyAppDbContext _context;
    private readonly IMapper _mapper;

    public GetTipoRecetaQueryHandler(MyAppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<GetTipoRecetaQueryResponse> Handle(GetTipoRecetaQuery request, CancellationToken cancellationToken)
    {
        var obj = await _context.TiposReceta.FindAsync(request.TipoRecetaId.FromHashId());

        if (obj is null)
        {
            throw new NotFoundException(nameof(Product), request.TipoRecetaId);
        }

        return _mapper.Map<GetTipoRecetaQueryResponse>(obj);
    }
}

public class GetTipoRecetaQueryResponse
{
    public string TipoRecetaId { get; set; } = default!;
    public string NombreVariable { get; set; } = default!;
    public string Descripcion { get; set; } = default!;
    public bool Estado { get; set; }
}

public class GetTipoRecetaQueryProfile : Profile
{
    public GetTipoRecetaQueryProfile() =>
        CreateMap<TipoReceta, GetTipoRecetaQueryResponse>()
            .ForMember(dest =>
                dest.TipoRecetaId,
                opt => opt.MapFrom(mf => mf.TipoRecetaId.ToHashId()));

}