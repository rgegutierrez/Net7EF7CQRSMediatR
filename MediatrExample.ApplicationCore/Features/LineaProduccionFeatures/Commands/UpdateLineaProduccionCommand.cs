using AutoMapper;
using FluentValidation;
using MediatR;
using MediatrExample.ApplicationCore.Common.Helpers;
using MediatrExample.ApplicationCore.Domain;
using MediatrExample.ApplicationCore.Infrastructure.Persistence;

namespace MediatrExample.ApplicationCore.Features.LineaProduccionFeatures.Commands;
public class UpdateLineaProduccionCommand : IRequest
{
    public string LineaProduccionId { get; set; } = default!;
    public string NombreVariable { get; set; } = default!;
    public string Descripcion { get; set; } = default!;
    public bool Estado { get; set; }
}

public class UpdateLineaProduccionCommandHandler : IRequestHandler<UpdateLineaProduccionCommand>
{
    private readonly MyAppDbContext _context;
    private readonly IMapper _mapper;

    public UpdateLineaProduccionCommandHandler(MyAppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(UpdateLineaProduccionCommand request, CancellationToken cancellationToken)
    {
        var updObj = _mapper.Map<LineaProduccion>(request);

        var oObj = _context.LineasProduccion.Find(updObj.LineaProduccionId);
        oObj.NombreVariable = updObj.NombreVariable;
        oObj.Descripcion = updObj.Descripcion;
        oObj.Estado = updObj.Estado;

        await _context.SaveChangesAsync();

        return Unit.Value;
    }
}
public class UpdateLineaProduccionCommandMapper : Profile
{
    public UpdateLineaProduccionCommandMapper() =>
        CreateMap<UpdateLineaProduccionCommand, LineaProduccion>()
            .ForMember(dest =>
                dest.LineaProduccionId,
                opt => opt.MapFrom(mf => mf.LineaProduccionId.FromHashId()));
}

public class UpdateLineaProduccionValidator : AbstractValidator<UpdateLineaProduccionCommand>
{
    public UpdateLineaProduccionValidator()
    {
        RuleFor(r => r.NombreVariable).NotNull();
        RuleFor(r => r.Descripcion).NotNull();
    }
}
