using AutoMapper;
using FluentValidation;
using MediatR;
using MediatrExample.ApplicationCore.Domain;
using MediatrExample.ApplicationCore.Infrastructure.Persistence;

namespace MediatrExample.ApplicationCore.Features.IndicadorPrensaFeatures.Commands;
public class CreateIndicadorPrensaCommand : IRequest
{
    public string NombreVariable { get; set; } = default!;
    public int TipoIndicadorPrensaId { get; set; }
    public string UnidadMedida { get; set; } = default!;
    public decimal ValorMinimo { get; set; }
    public decimal ValorMaximo { get; set; }
    public bool Obligatoria { get; set; }
    public bool Estado { get; set; }
}

public class CreateIndicadorPrensaCommandHandler : IRequestHandler<CreateIndicadorPrensaCommand>
{
    private readonly MyAppDbContext _context;
    private readonly IMapper _mapper;

    public CreateIndicadorPrensaCommandHandler(MyAppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(CreateIndicadorPrensaCommand request, CancellationToken cancellationToken)
    {
        var newIndicadorPrensa = _mapper.Map<IndicadorPrensa>(request);

        _context.IndicadoresPrensa.Add(newIndicadorPrensa);

        await _context.SaveChangesAsync();

        return Unit.Value;
    }
}
public class CreateIndicadorPrensaCommandMapper : Profile
{
    public CreateIndicadorPrensaCommandMapper() =>
        CreateMap<CreateIndicadorPrensaCommand, IndicadorPrensa>();
}

public class CreateIndicadorPrensaValidator : AbstractValidator<CreateIndicadorPrensaCommand>
{
    public CreateIndicadorPrensaValidator()
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
