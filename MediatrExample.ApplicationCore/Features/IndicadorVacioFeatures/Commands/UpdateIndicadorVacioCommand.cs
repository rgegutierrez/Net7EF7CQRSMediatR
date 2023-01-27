using AutoMapper;
using FluentValidation;
using MediatR;
using MediatrExample.ApplicationCore.Common.Helpers;
using MediatrExample.ApplicationCore.Domain;
using MediatrExample.ApplicationCore.Infrastructure.Persistence;

namespace MediatrExample.ApplicationCore.Features.IndicadorVacioFeatures.Commands;
public class UpdateIndicadorVacioCommand : IRequest
{
    public string IndicadorVacioId { get; set; } = default!;
    public string NombreVariable { get; set; } = default!;
    public int TipoIndicadorVacioId { get; set; }
    public string UnidadMedida { get; set; } = default!;
    public decimal ValorMinimo { get; set; }
    public decimal ValorMaximo { get; set; }
    public bool Obligatoria { get; set; }
    public bool Estado { get; set; }
}

public class UpdateIndicadorVacioCommandHandler : IRequestHandler<UpdateIndicadorVacioCommand>
{
    private readonly MyAppDbContext _context;
    private readonly IMapper _mapper;

    public UpdateIndicadorVacioCommandHandler(MyAppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(UpdateIndicadorVacioCommand request, CancellationToken cancellationToken)
    {
        var updObj = _mapper.Map<IndicadorVacio>(request);

        var oObj = _context.IndicadoresVacio.Find(updObj.IndicadorVacioId);
        oObj.NombreVariable = updObj.NombreVariable;
        oObj.TipoIndicadorVacioId = updObj.TipoIndicadorVacioId;
        oObj.UnidadMedida = updObj.UnidadMedida;
        oObj.ValorMinimo = updObj.ValorMinimo;
        oObj.ValorMaximo = updObj.ValorMaximo;
        oObj.Obligatoria = updObj.Obligatoria;
        oObj.Estado = updObj.Estado;

        await _context.SaveChangesAsync();

        return Unit.Value;
    }
}
public class UpdateIndicadorVacioCommandMapper : Profile
{
    public UpdateIndicadorVacioCommandMapper() =>
        CreateMap<UpdateIndicadorVacioCommand, IndicadorVacio>()
            .ForMember(dest =>
                dest.IndicadorVacioId,
                opt => opt.MapFrom(mf => mf.IndicadorVacioId.FromHashId()));
}

public class UpdateIndicadorVacioValidator : AbstractValidator<UpdateIndicadorVacioCommand>
{
    public UpdateIndicadorVacioValidator()
    {
        RuleFor(r => r.NombreVariable).NotNull();
        RuleFor(r => r.TipoIndicadorVacioId).NotNull();
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
