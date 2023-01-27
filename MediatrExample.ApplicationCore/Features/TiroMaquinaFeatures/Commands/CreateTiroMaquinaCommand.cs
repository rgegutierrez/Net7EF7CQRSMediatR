using AutoMapper;
using FluentValidation;
using MediatR;
using MediatrExample.ApplicationCore.Common.Helpers;
using MediatrExample.ApplicationCore.Domain;
using MediatrExample.ApplicationCore.Infrastructure.Persistence;

namespace MediatrExample.ApplicationCore.Features.TiroMaquinaFeatures.Commands;
public class CreateTiroMaquinaCommand : IRequest
{
    public string NombreVariable { get; set; } = default!;
    public string UnidadMedida { get; set; } = default!;
    public decimal ValorMinimo { get; set; }
    public decimal ValorMaximo { get; set; }
    public bool Obligatoria { get; set; }
    public bool Estado { get; set; }
}

public class CreateTiroMaquinaCommandHandler : IRequestHandler<CreateTiroMaquinaCommand>
{
    private readonly MyAppDbContext _context;
    private readonly IMapper _mapper;

    public CreateTiroMaquinaCommandHandler(MyAppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(CreateTiroMaquinaCommand request, CancellationToken cancellationToken)
    {
        var crtObj = _mapper.Map<TiroMaquina>(request);

        _context.TirosMaquina.Add(crtObj);

        await _context.SaveChangesAsync();

        return Unit.Value;
    }
}
public class CreateTiroMaquinaCommandMapper : Profile
{
    public CreateTiroMaquinaCommandMapper() =>
        CreateMap<CreateTiroMaquinaCommand, TiroMaquina>();
}

public class CreateTiroMaquinaValidator : AbstractValidator<CreateTiroMaquinaCommand>
{
    public CreateTiroMaquinaValidator()
    {
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
