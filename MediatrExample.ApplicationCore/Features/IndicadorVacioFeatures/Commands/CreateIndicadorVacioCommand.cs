using AutoMapper;
using FluentValidation;
using MediatR;
using MediatrExample.ApplicationCore.Domain;
using MediatrExample.ApplicationCore.Infrastructure.Persistence;

namespace MediatrExample.ApplicationCore.Features.IndicadorVacioFeatures.Commands;
public class CreateIndicadorVacioCommand : IRequest
{
    public string NombreVariable { get; set; } = default!;
    public int TipoIndicadorVacioId { get; set; }
    public string UnidadMedida { get; set; } = default!;
    public decimal ValorMinimo { get; set; }
    public decimal ValorMaximo { get; set; }
    public bool Obligatoria { get; set; }
    public bool Estado { get; set; }
}

public class CreateIndicadorVacioCommandHandler : IRequestHandler<CreateIndicadorVacioCommand>
{
    private readonly MyAppDbContext _context;
    private readonly IMapper _mapper;

    public CreateIndicadorVacioCommandHandler(MyAppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(CreateIndicadorVacioCommand request, CancellationToken cancellationToken)
    {
        var newIndicadorVacio = _mapper.Map<IndicadorVacio>(request);

        _context.IndicadoresVacio.Add(newIndicadorVacio);

        await _context.SaveChangesAsync();

        return Unit.Value;
    }
}
public class CreateIndicadorVacioCommandMapper : Profile
{
    public CreateIndicadorVacioCommandMapper() =>
        CreateMap<CreateIndicadorVacioCommand, IndicadorVacio>();
}

public class CreateIndicadorVacioValidator : AbstractValidator<CreateIndicadorVacioCommand>
{
    public CreateIndicadorVacioValidator()
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
