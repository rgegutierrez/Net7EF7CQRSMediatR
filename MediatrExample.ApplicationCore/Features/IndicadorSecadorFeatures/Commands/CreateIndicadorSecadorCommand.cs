using AutoMapper;
using FluentValidation;
using MediatR;
using MediatrExample.ApplicationCore.Domain;
using MediatrExample.ApplicationCore.Infrastructure.Persistence;

namespace MediatrExample.ApplicationCore.Features.IndicadorSecadorFeatures.Commands;
public class CreateIndicadorSecadorCommand : IRequest
{
    public string NombreVariable { get; set; } = default!;
    public int TipoIndicadorSecadorId { get; set; }
    public string UnidadMedida { get; set; } = default!;
    public decimal ValorMinimo { get; set; }
    public decimal ValorMaximo { get; set; }
    public bool Obligatoria { get; set; }
    public bool Estado { get; set; }
}

public class CreateIndicadorSecadorCommandHandler : IRequestHandler<CreateIndicadorSecadorCommand>
{
    private readonly MyAppDbContext _context;
    private readonly IMapper _mapper;

    public CreateIndicadorSecadorCommandHandler(MyAppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(CreateIndicadorSecadorCommand request, CancellationToken cancellationToken)
    {
        var newIndicadorSecador = _mapper.Map<IndicadorSecador>(request);

        _context.IndicadoresSecador.Add(newIndicadorSecador);

        await _context.SaveChangesAsync();

        return Unit.Value;
    }
}
public class CreateIndicadorSecadorCommandMapper : Profile
{
    public CreateIndicadorSecadorCommandMapper() =>
        CreateMap<CreateIndicadorSecadorCommand, IndicadorSecador>();
}

public class CreateIndicadorSecadorValidator : AbstractValidator<CreateIndicadorSecadorCommand>
{
    public CreateIndicadorSecadorValidator()
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
