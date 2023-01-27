using AutoMapper;
using FluentValidation;
using MediatR;
using MediatrExample.ApplicationCore.Common.Helpers;
using MediatrExample.ApplicationCore.Domain;
using MediatrExample.ApplicationCore.Infrastructure.Persistence;

namespace MediatrExample.ApplicationCore.Features.IndicadorSecadorFeatures.Commands;
public class UpdateIndicadorSecadorCommand : IRequest
{
    public string IndicadorSecadorId { get; set; } = default!;
    public string NombreVariable { get; set; } = default!;
    public int TipoIndicadorSecadorId { get; set; }
    public string UnidadMedida { get; set; } = default!;
    public decimal ValorMinimo { get; set; }
    public decimal ValorMaximo { get; set; }
    public bool Obligatoria { get; set; }
    public bool Estado { get; set; }
}

public class UpdateIndicadorSecadorCommandHandler : IRequestHandler<UpdateIndicadorSecadorCommand>
{
    private readonly MyAppDbContext _context;
    private readonly IMapper _mapper;

    public UpdateIndicadorSecadorCommandHandler(MyAppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(UpdateIndicadorSecadorCommand request, CancellationToken cancellationToken)
    {
        var updObj = _mapper.Map<IndicadorSecador>(request);

        var oObj = _context.IndicadoresSecador.Find(updObj.IndicadorSecadorId);
        oObj.NombreVariable = updObj.NombreVariable;
        oObj.TipoIndicadorSecadorId = updObj.TipoIndicadorSecadorId;
        oObj.UnidadMedida = updObj.UnidadMedida;
        oObj.ValorMinimo = updObj.ValorMinimo;
        oObj.ValorMaximo = updObj.ValorMaximo;
        oObj.Obligatoria = updObj.Obligatoria;
        oObj.Estado = updObj.Estado;

        await _context.SaveChangesAsync();

        return Unit.Value;
    }
}
public class UpdateIndicadorSecadorCommandMapper : Profile
{
    public UpdateIndicadorSecadorCommandMapper() =>
        CreateMap<UpdateIndicadorSecadorCommand, IndicadorSecador>()
            .ForMember(dest =>
                dest.IndicadorSecadorId,
                opt => opt.MapFrom(mf => mf.IndicadorSecadorId.FromHashId()));
}

public class UpdateIndicadorSecadorValidator : AbstractValidator<UpdateIndicadorSecadorCommand>
{
    public UpdateIndicadorSecadorValidator()
    {
        RuleFor(r => r.NombreVariable).NotNull();
        RuleFor(r => r.TipoIndicadorSecadorId).NotNull();
        RuleFor(r => r.UnidadMedida).NotNull();
        RuleFor(r => r.ValorMinimo).NotNull();
        RuleFor(r => r.ValorMaximo).NotNull();
        RuleFor(r => new { r.ValorMinimo, r.ValorMaximo })
            .Must(v => v.ValorMinimo < v.ValorMaximo)
            .WithMessage("Valor Mínimo debe ser menor que Valor Máximo");
        RuleFor(r => r.Obligatoria).NotNull();
        RuleFor(r => r.Estado).NotNull();
    }
}
