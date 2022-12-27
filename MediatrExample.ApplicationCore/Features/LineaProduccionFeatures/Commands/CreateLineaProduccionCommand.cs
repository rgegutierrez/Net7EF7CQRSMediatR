using AutoMapper;
using FluentValidation;
using MediatR;
using MediatrExample.ApplicationCore.Domain;
using MediatrExample.ApplicationCore.Infrastructure.Persistence;

namespace MediatrExample.ApplicationCore.Features.LineaProduccionFeatures.Commands;
public class CreateLineaProduccionCommand : IRequest
{
    public string NombreVariable { get; set; } = default!;
    public string Descripcion { get; set; } = default!;
    public bool Estado { get; set; }
}

public class CreateLineaProduccionCommandHandler : IRequestHandler<CreateLineaProduccionCommand>
{
    private readonly MyAppDbContext _context;
    private readonly IMapper _mapper;

    public CreateLineaProduccionCommandHandler(MyAppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(CreateLineaProduccionCommand request, CancellationToken cancellationToken)
    {
        var newLineaProduccion = _mapper.Map<LineaProduccion>(request);

        _context.LineasProduccion.Add(newLineaProduccion);

        await _context.SaveChangesAsync();

        return Unit.Value;
    }
}
public class CreateLineaProduccionCommandMapper : Profile
{
    public CreateLineaProduccionCommandMapper() =>
        CreateMap<CreateLineaProduccionCommand, LineaProduccion>();
}

public class CreateLineaProduccionValidator : AbstractValidator<CreateLineaProduccionCommand>
{
    public CreateLineaProduccionValidator()
    {
        RuleFor(r => r.NombreVariable).NotNull();
        RuleFor(r => r.Descripcion).NotNull();
    }
}
