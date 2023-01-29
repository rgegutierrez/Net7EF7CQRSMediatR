using AutoMapper;
using FluentValidation;
using MediatR;
using MediatrExample.ApplicationCore.Common.Helpers;
using MediatrExample.ApplicationCore.Domain;
using MediatrExample.ApplicationCore.Infrastructure.Persistence;

namespace MediatrExample.ApplicationCore.Features.ValorFisicoPieMaquinaFeatures.Commands;
public class CreateValorFisicoPieMaquinaCommand : IRequest
{
    public string NombreVariable { get; set; } = default!;
    public string UnidadMedida { get; set; } = default!;
    public decimal ValorMinimo { get; set; }
    public decimal ValorMaximo { get; set; }
    public bool Obligatoria { get; set; }
    public bool Estado { get; set; }
}

public class CreateValorFisicoPieMaquinaCommandHandler : IRequestHandler<CreateValorFisicoPieMaquinaCommand>
{
    private readonly MyAppDbContext _context;
    private readonly IMapper _mapper;

    public CreateValorFisicoPieMaquinaCommandHandler(MyAppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(CreateValorFisicoPieMaquinaCommand request, CancellationToken cancellationToken)
    {
        var crtObj = _mapper.Map<ValorFisicoPieMaquina>(request);

        _context.ValoresFisicoPieMaquina.Add(crtObj);

        await _context.SaveChangesAsync();

        return Unit.Value;
    }
}
public class CreateValorFisicoPieMaquinaCommandMapper : Profile
{
    public CreateValorFisicoPieMaquinaCommandMapper() =>
        CreateMap<CreateValorFisicoPieMaquinaCommand, ValorFisicoPieMaquina>();
}

public class CreateValorFisicoPieMaquinaValidator : AbstractValidator<CreateValorFisicoPieMaquinaCommand>
{
    public CreateValorFisicoPieMaquinaValidator()
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
