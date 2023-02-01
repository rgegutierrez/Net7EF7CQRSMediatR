﻿using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using MediatrExample.ApplicationCore.Common.Helpers;
using MediatrExample.ApplicationCore.Domain;
using MediatrExample.ApplicationCore.Domain.View;
using MediatrExample.ApplicationCore.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace MediatrExample.ApplicationCore.Features.EstandarFeatures.Queries;

public class GetListEstandarQuery : IRequest<List<GetListEstandarQueryResponse>>
{

}

public class GetListEstandarQueryHandler : IRequestHandler<GetListEstandarQuery, List<GetListEstandarQueryResponse>>
{
    private readonly MyAppDbContext _context;
    private readonly IMapper _mapper;

    public GetListEstandarQueryHandler(MyAppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public Task<List<GetListEstandarQueryResponse>> Handle(GetListEstandarQuery request, CancellationToken cancellationToken) =>
        _context.EstandaresVW
            .AsNoTracking()
            .ProjectTo<GetListEstandarQueryResponse>(_mapper.ConfigurationProvider)
            .ToListAsync();
}

public class GetListEstandarQueryResponse
{
    public string EstandarId { get; set; } = default!;
    public string ClienteId { get; set; }
    public string ClienteNombre { get; set; }
    public string TipoPapelId { get; set; }
    public string TipoPapelNombre { get; set; }
    public int ValorFisicoPieMaquinaId { get; set; }
    public string ValorFisicoPieMaquinaNombre { get; set; }
    public decimal ValorMinimo { get; set; }
    public decimal ValorPromedio { get; set; }
    public decimal ValorMaximo { get; set; }
    public string ValorMinimoStr { get; set; }
    public string ValorPromedioStr { get; set; }
    public string ValorMaximoStr { get; set; }
}

public class GetListEstandarQueryProfile : Profile
{
    public GetListEstandarQueryProfile() =>
        CreateMap<EstandarVW, GetListEstandarQueryResponse>()
            .ForMember(dest =>
                dest.EstandarId,
                opt => opt.MapFrom(mf => mf.EstandarId.ToHashId()))
            .ForMember(dest =>
                dest.ValorMinimoStr,
                opt => opt.MapFrom(mf => mf.ValorMinimo.FromDotToComma()))
            .ForMember(dest =>
                dest.ValorMaximoStr,
                opt => opt.MapFrom(mf => mf.ValorMaximo.FromDotToComma()))
            .ForMember(dest =>
                dest.ValorPromedioStr,
                opt => opt.MapFrom(mf => mf.ValorPromedio.FromDotToComma()));

}
