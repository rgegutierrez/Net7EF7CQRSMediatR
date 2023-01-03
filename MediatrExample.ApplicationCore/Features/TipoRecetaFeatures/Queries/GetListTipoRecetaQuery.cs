using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using MediatrExample.ApplicationCore.Common.Helpers;
using MediatrExample.ApplicationCore.Domain;
using MediatrExample.ApplicationCore.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace MediatrExample.ApplicationCore.Features.TipoRecetaFeatures.Queries;

public class GetListTipoRecetaQuery : IRequest<List<GetListTipoRecetaQueryResponse>>
{

}

public class GetListTipoRecetaQueryHandler : IRequestHandler<GetListTipoRecetaQuery, List<GetListTipoRecetaQueryResponse>>
{
    private readonly MyAppDbContext _context;
    private readonly IMapper _mapper;

    public GetListTipoRecetaQueryHandler(MyAppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public Task<List<GetListTipoRecetaQueryResponse>> Handle(GetListTipoRecetaQuery request, CancellationToken cancellationToken) =>
        _context.TiposReceta
            .AsNoTracking()
            .ProjectTo<GetListTipoRecetaQueryResponse>(_mapper.ConfigurationProvider)
            .ToListAsync();
}

public class GetListTipoRecetaQueryResponse
{
    public string TipoRecetaId { get; set; } = default!;
    public string NombreVariable { get; set; } = default!;
    public string Descripcion { get; set; } = default!;
    public bool Estado { get; set; }
}

public class GetListTipoRecetaQueryProfile : Profile
{
    public GetListTipoRecetaQueryProfile() =>
        CreateMap<TipoReceta, GetListTipoRecetaQueryResponse>()
            .ForMember(dest =>
                dest.TipoRecetaId,
                opt => opt.MapFrom(mf => mf.TipoRecetaId.ToHashId()));

}
