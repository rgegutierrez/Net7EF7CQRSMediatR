using AutoMapper;
using FluentValidation;
using MediatR;
using MediatrExample.ApplicationCore.Domain;
using MediatrExample.ApplicationCore.Infrastructure.Persistence;

namespace MediatrExample.ApplicationCore.Features.TipoRecetaFeatures.Commands;
public class CreateTipoRecetaCommand : IRequest
{
    public string NombreVariable { get; set; } = default!;
    public string Descripcion { get; set; } = default!;
    public bool Estado { get; set; }
}

public class CreateTipoRecetaCommandHandler : IRequestHandler<CreateTipoRecetaCommand>
{
    private readonly MyAppDbContext _context;
    private readonly IMapper _mapper;

    public CreateTipoRecetaCommandHandler(MyAppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(CreateTipoRecetaCommand request, CancellationToken cancellationToken)
    {
        var newTipoReceta = _mapper.Map<TipoReceta>(request);

        _context.TiposReceta.Add(newTipoReceta);

        await _context.SaveChangesAsync();

        return Unit.Value;
    }
}
public class CreateTipoRecetaCommandMapper : Profile
{
    public CreateTipoRecetaCommandMapper() =>
        CreateMap<CreateTipoRecetaCommand, TipoReceta>();
}

public class CreateTipoRecetaValidator : AbstractValidator<CreateTipoRecetaCommand>
{
    public CreateTipoRecetaValidator()
    {
        RuleFor(r => r.NombreVariable).NotNull();
        RuleFor(r => r.Descripcion).NotNull();
    }
}
