using AutoMapper;
using FluentValidation;
using MediatR;
using MediatrExample.ApplicationCore.Domain;
using MediatrExample.ApplicationCore.Infrastructure.Persistence;

namespace MediatrExample.ApplicationCore.Features.EstandarFeatures.Commands;
public class CreateEstandarCommand : IRequest
{
    public string ClienteId { get; set; }
    public string TipoPapelId { get; set; }
    public int ValorFisicoPieMaquinaId { get; set; }
    public decimal ValorMinimo { get; set; }
    public decimal ValorPromedio { get; set; }
    public decimal? ValorMaximo { get; set; }
}

public class CreateEstandarCommandHandler : IRequestHandler<CreateEstandarCommand>
{
    private readonly MyAppDbContext _context;
    private readonly IMapper _mapper;

    public CreateEstandarCommandHandler(MyAppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(CreateEstandarCommand request, CancellationToken cancellationToken)
    {
        var newEstandar = _mapper.Map<Estandar>(request);

        _context.Estandares.Add(newEstandar);

        await _context.SaveChangesAsync();

        return Unit.Value;
    }
}
public class CreateEstandarCommandMapper : Profile
{
    public CreateEstandarCommandMapper() =>
        CreateMap<CreateEstandarCommand, Estandar>();
}

public class CreateEstandarValidator : AbstractValidator<CreateEstandarCommand>
{
    public CreateEstandarValidator()
    {
        RuleFor(r => r.ClienteId).NotNull();
        RuleFor(r => r.TipoPapelId).NotNull();
        RuleFor(r => r.ValorFisicoPieMaquinaId).NotNull();
        RuleFor(r => r.ValorMinimo).NotNull();
        RuleFor(r => r.ValorPromedio).NotNull();
    }
}
