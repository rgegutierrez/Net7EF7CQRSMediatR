using AutoMapper;
using FluentValidation;
using MediatR;
using MediatrExample.ApplicationCore.Common.Helpers;
using MediatrExample.ApplicationCore.Domain;
using MediatrExample.ApplicationCore.Infrastructure.Persistence;

namespace MediatrExample.ApplicationCore.Features.TipoRecetaFeatures.Commands;
public class UpdateTipoRecetaCommand : IRequest
{
    public string TipoRecetaId { get; set; } = default!;
    public string NombreVariable { get; set; } = default!;
    public string Descripcion { get; set; } = default!;
    public bool Estado { get; set; }
}

public class UpdateTipoRecetaCommandHandler : IRequestHandler<UpdateTipoRecetaCommand>
{
    private readonly MyAppDbContext _context;
    private readonly IMapper _mapper;

    public UpdateTipoRecetaCommandHandler(MyAppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(UpdateTipoRecetaCommand request, CancellationToken cancellationToken)
    {
        var updObj = _mapper.Map<TipoReceta>(request);

        var oObj = _context.TiposReceta.Find(updObj.TipoRecetaId);
        oObj.NombreVariable = updObj.NombreVariable;
        oObj.Descripcion = updObj.Descripcion;
        oObj.Estado = updObj.Estado;

        await _context.SaveChangesAsync();

        return Unit.Value;
    }
}
public class UpdateTipoRecetaCommandMapper : Profile
{
    public UpdateTipoRecetaCommandMapper() =>
        CreateMap<UpdateTipoRecetaCommand, TipoReceta>()
            .ForMember(dest =>
                dest.TipoRecetaId,
                opt => opt.MapFrom(mf => mf.TipoRecetaId.FromHashId()));
}

public class UpdateTipoRecetaValidator : AbstractValidator<UpdateTipoRecetaCommand>
{
    public UpdateTipoRecetaValidator()
    {
        RuleFor(r => r.NombreVariable).NotNull();
        RuleFor(r => r.Descripcion).NotNull();
    }
}
