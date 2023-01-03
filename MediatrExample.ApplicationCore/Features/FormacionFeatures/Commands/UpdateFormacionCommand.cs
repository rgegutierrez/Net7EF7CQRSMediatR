using AutoMapper;
using FluentValidation;
using MediatR;
using MediatrExample.ApplicationCore.Common.Helpers;
using MediatrExample.ApplicationCore.Domain;
using MediatrExample.ApplicationCore.Infrastructure.Persistence;

namespace MediatrExample.ApplicationCore.Features.FormacionFeatures.Commands;
public class UpdateFormacionCommand : IRequest
{
    public string FormacionId { get; set; } = default!;
    public string NombreVariable { get; set; } = default!;
    public string UnidadMedidaAngulo { get; set; } = default!;
    public decimal RangoAnguloMinimo { get; set; }
    public decimal RangoAnguloMaximo { get; set; }
    public string UnidadMedidaAltura { get; set; } = default!;
    public decimal RangoAlturaMinimo { get; set; }
    public decimal RangoAlturaMaximo { get; set; }
    public bool Obligatoria { get; set; }
    public bool Estado { get; set; }
}

public class UpdateFormacionCommandHandler : IRequestHandler<UpdateFormacionCommand>
{
    private readonly MyAppDbContext _context;
    private readonly IMapper _mapper;

    public UpdateFormacionCommandHandler(MyAppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(UpdateFormacionCommand request, CancellationToken cancellationToken)
    {
        var updObj = _mapper.Map<Formacion>(request);

        var oObj = _context.Formaciones.Find(updObj.FormacionId);
        oObj.NombreVariable = updObj.NombreVariable;
        oObj.UnidadMedidaAngulo = updObj.UnidadMedidaAngulo;
        oObj.RangoAnguloMinimo = updObj.RangoAnguloMinimo;
        oObj.RangoAnguloMaximo = updObj.RangoAnguloMaximo;
        oObj.UnidadMedidaAltura = updObj.UnidadMedidaAltura;
        oObj.RangoAlturaMinimo = updObj.RangoAlturaMinimo;
        oObj.RangoAlturaMaximo = updObj.RangoAlturaMaximo;
        oObj.Obligatoria = updObj.Obligatoria;
        oObj.Estado = updObj.Estado;

        await _context.SaveChangesAsync();

        return Unit.Value;
    }
}
public class UpdateFormacionCommandMapper : Profile
{
    public UpdateFormacionCommandMapper() =>
        CreateMap<UpdateFormacionCommand, Formacion>()
            .ForMember(dest =>
                dest.FormacionId,
                opt => opt.MapFrom(mf => mf.FormacionId.FromHashId()));
}

public class UpdateFormacionValidator : AbstractValidator<UpdateFormacionCommand>
{
    public UpdateFormacionValidator()
    {
        RuleFor(r => r.NombreVariable).NotNull();
        RuleFor(r => r.UnidadMedidaAngulo).NotNull();
        RuleFor(r => r.RangoAnguloMinimo).NotNull();
        RuleFor(r => r.RangoAnguloMaximo).NotNull();
        RuleFor(r => r.UnidadMedidaAltura).NotNull();
        RuleFor(r => r.RangoAlturaMinimo).NotNull();
        RuleFor(r => r.RangoAlturaMaximo).NotNull();
    }
}
