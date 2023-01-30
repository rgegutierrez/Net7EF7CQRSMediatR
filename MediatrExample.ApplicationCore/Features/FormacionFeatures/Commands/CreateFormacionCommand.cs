using AutoMapper;
using FluentValidation;
using MediatR;
using MediatrExample.ApplicationCore.Domain;
using MediatrExample.ApplicationCore.Infrastructure.Persistence;

namespace MediatrExample.ApplicationCore.Features.FormacionFeatures.Commands;
public class CreateFormacionCommand : IRequest
{
    public string NombreVariable { get; set; } = default!;
    public string UnidadMedidaAngulo { get; set; } = default!;
    public decimal RangoAnguloMinimo { get; set; }
    public decimal RangoAnguloMaximo { get; set; }
    public string UnidadMedidaAltura { get; set; } = default!;
    public decimal RangoAlturaMinimo { get; set; }
    public decimal RangoAlturaMaximo { get; set; }
    public bool Obligatoria { get; set; }
    public bool Estado { get; set; }
}

public class CreateFormacionCommandHandler : IRequestHandler<CreateFormacionCommand>
{
    private readonly MyAppDbContext _context;
    private readonly IMapper _mapper;

    public CreateFormacionCommandHandler(MyAppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(CreateFormacionCommand request, CancellationToken cancellationToken)
    {
        var newFormacion = _mapper.Map<Formacion>(request);

        _context.Formaciones.Add(newFormacion);

        await _context.SaveChangesAsync();

        return Unit.Value;
    }
}
public class CreateFormacionCommandMapper : Profile
{
    public CreateFormacionCommandMapper() =>
        CreateMap<CreateFormacionCommand, Formacion>();
}

public class CreateFormacionValidator : AbstractValidator<CreateFormacionCommand>
{
    public CreateFormacionValidator()
    {
        RuleFor(r => r.NombreVariable).NotNull();
        RuleFor(r => r.UnidadMedidaAngulo).NotNull();
        RuleFor(r => r.RangoAnguloMinimo).NotNull();
        RuleFor(r => r.RangoAnguloMaximo).NotNull();
        RuleFor(r => r.UnidadMedidaAltura).NotNull();
        RuleFor(r => r.RangoAlturaMinimo).NotNull();
        RuleFor(r => r.RangoAlturaMaximo).NotNull();
    }
}
