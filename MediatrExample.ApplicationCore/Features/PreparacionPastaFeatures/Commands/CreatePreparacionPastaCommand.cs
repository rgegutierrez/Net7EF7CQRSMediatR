using AutoMapper;
using FluentValidation;
using MediatR;
using MediatrExample.ApplicationCore.Domain;
using MediatrExample.ApplicationCore.Features.PreparacionPastaFeatures.Queries;
using MediatrExample.ApplicationCore.Infrastructure.Persistence;
using Microsoft.Extensions.DependencyInjection;

namespace MediatrExample.ApplicationCore.Features.PreparacionPastaFeatures.Commands;
public class CreatePreparacionPastaCommand : IRequest
{
    public string NombreVariable { get; set; } = default!;
    public string UnidadMedida { get; set; } = default!;
    public int ValorMinimo { get; set; }
    public int ValorMaximo { get; set; }
    public bool Obligatoria { get; set; }
    public bool Estado { get; set; }
}

public class CreatePreparacionPastaCommandHandler : IRequestHandler<CreatePreparacionPastaCommand>
{
    private readonly MyAppDbContext _context;
    private readonly IMapper _mapper;

    public CreatePreparacionPastaCommandHandler(MyAppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(CreatePreparacionPastaCommand request, CancellationToken cancellationToken)
    {
        var crtObj = _mapper.Map<PreparacionPasta>(request);

        _context.PreparacionPastas.Add(crtObj);

        await _context.SaveChangesAsync();

        return Unit.Value;
    }
}
public class CreatePreparacionPastaCommandMapper : Profile
{
    public CreatePreparacionPastaCommandMapper() =>
        CreateMap<CreatePreparacionPastaCommand, PreparacionPasta>();
}

public class CreatePreparacionPastaValidator : AbstractValidator<CreatePreparacionPastaCommand>
{
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly IMediator _mediator;

    public CreatePreparacionPastaValidator(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
        var scope = _serviceScopeFactory.CreateScope();
        _mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

        RuleFor(t => new CheckPreparacionPasteByNombreVariable
        {
            NombreVariable = t.NombreVariable
        }).NotEmpty().Must(BeUnique).WithName("NombreVariable").WithMessage(r => $"Ya existe un registro con el mismo Nombre de Variable: {r.NombreVariable}"); ;
        RuleFor(r => r.NombreVariable).NotNull().MaximumLength(100);
        RuleFor(r => r.UnidadMedida).NotNull();
        RuleFor(r => r.ValorMinimo)
            .NotNull()
            .GreaterThanOrEqualTo(0)
            .Must(v => v.ToString().Length <= 10)
            .WithMessage("Valor Mínimo no puede tener más de 10 dígitos");
        RuleFor(r => r.ValorMaximo)
            .NotNull()
            .GreaterThanOrEqualTo(0)
            .Must(v => v.ToString().Length <= 10)
            .WithMessage("Valor Máximo no puede tener más de 10 dígitos");
        RuleFor(r => new { r.ValorMinimo, r.ValorMaximo })
            .Must(v => v.ValorMinimo < v.ValorMaximo)
            .WithMessage("Valor Mínimo debe ser menor que Valor Máximo");
        RuleFor(r => r.Obligatoria).NotNull();
        RuleFor(r => r.Estado).NotNull();
    }

    private bool BeUnique(CheckPreparacionPasteByNombreVariable command)
    {
        var aux = _mediator.Send(command);

        return aux.Result.Check;
    }
}
