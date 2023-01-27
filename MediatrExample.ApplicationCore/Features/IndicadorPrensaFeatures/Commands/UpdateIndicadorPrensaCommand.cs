using AutoMapper;
using FluentValidation;
using MediatR;
using MediatrExample.ApplicationCore.Common.Helpers;
using MediatrExample.ApplicationCore.Domain;
using MediatrExample.ApplicationCore.Infrastructure.Persistence;

namespace MediatrExample.ApplicationCore.Features.IndicadorPrensaFeatures.Commands;
public class UpdateIndicadorPrensaCommand : IRequest
{
    public string IndicadorPrensaId { get; set; } = default!;
    public string NombreVariable { get; set; } = default!;
    public int TipoIndicadorPrensaId { get; set; }
    public string UnidadMedida { get; set; } = default!;
    public decimal ValorMinimo { get; set; }
    public decimal ValorMaximo { get; set; }
    public bool Obligatoria { get; set; }
    public bool Estado { get; set; }
}

public class UpdateIndicadorPrensaCommandHandler : IRequestHandler<UpdateIndicadorPrensaCommand>
{
    private readonly MyAppDbContext _context;
    private readonly IMapper _mapper;

    public UpdateIndicadorPrensaCommandHandler(MyAppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(UpdateIndicadorPrensaCommand request, CancellationToken cancellationToken)
    {
        var updObj = _mapper.Map<IndicadorPrensa>(request);

        var oObj = _context.IndicadoresPrensa.Find(updObj.IndicadorPrensaId);
        oObj.NombreVariable = updObj.NombreVariable;
        oObj.TipoIndicadorPrensaId = updObj.TipoIndicadorPrensaId;
        oObj.UnidadMedida = updObj.UnidadMedida;
        oObj.ValorMinimo = updObj.ValorMinimo;
        oObj.ValorMaximo = updObj.ValorMaximo;
        oObj.Obligatoria = updObj.Obligatoria;
        oObj.Estado = updObj.Estado;

        await _context.SaveChangesAsync();

        return Unit.Value;
    }
}
public class UpdateIndicadorPrensaCommandMapper : Profile
{
    public UpdateIndicadorPrensaCommandMapper() =>
        CreateMap<UpdateIndicadorPrensaCommand, IndicadorPrensa>()
            .ForMember(dest =>
                dest.IndicadorPrensaId,
                opt => opt.MapFrom(mf => mf.IndicadorPrensaId.FromHashId()));
}

public class UpdateIndicadorPrensaValidator : AbstractValidator<UpdateIndicadorPrensaCommand>
{
    public UpdateIndicadorPrensaValidator()
    {
        RuleFor(r => r.NombreVariable).NotNull();
        RuleFor(r => r.TipoIndicadorPrensaId).NotNull();
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
