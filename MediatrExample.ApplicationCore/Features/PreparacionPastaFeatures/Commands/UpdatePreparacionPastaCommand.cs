using AutoMapper;
using FluentValidation;
using MediatR;
using MediatrExample.ApplicationCore.Common.Helpers;
using MediatrExample.ApplicationCore.Domain;
using MediatrExample.ApplicationCore.Features.PreparacionPastaFeatures.Queries;
using MediatrExample.ApplicationCore.Infrastructure.Persistence;
using Microsoft.Extensions.DependencyInjection;

namespace MediatrExample.ApplicationCore.Features.PreparacionPastaFeatures.Commands;
public class UpdatePreparacionPastaCommand : IRequest
{
    public string PreparacionPastaId { get; set; } = default!;
    public string NombreVariable { get; set; } = default!;
    public string UnidadMedida { get; set; } = default!;
    public int ValorMinimo { get; set; }
    public int ValorMaximo { get; set; }
    public bool Obligatoria { get; set; }
    public bool Estado { get; set; }
}

public class UpdatePreparacionPastaCommandHandler : IRequestHandler<UpdatePreparacionPastaCommand>
{
    private readonly MyAppDbContext _context;
    private readonly IMapper _mapper;

    public UpdatePreparacionPastaCommandHandler(MyAppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(UpdatePreparacionPastaCommand request, CancellationToken cancellationToken)
    {
        var updObj = _mapper.Map<PreparacionPasta>(request);

        var oObj = _context.PreparacionPastas.Find(updObj.PreparacionPastaId);
        oObj.UnidadMedida = updObj.UnidadMedida;
        oObj.ValorMinimo = updObj.ValorMinimo;
        oObj.ValorMaximo = updObj.ValorMaximo;
        oObj.Obligatoria = updObj.Obligatoria;
        oObj.Estado = updObj.Estado;

        await _context.SaveChangesAsync();

        return Unit.Value;
    }
}
public class UpdatePreparacionPastaCommandMapper : Profile
{
    public UpdatePreparacionPastaCommandMapper() =>
        CreateMap<UpdatePreparacionPastaCommand, PreparacionPasta>()
            .ForMember(dest =>
                dest.PreparacionPastaId,
                opt => opt.MapFrom(mf => mf.PreparacionPastaId.FromHashId()));
}

public class UpdatePreparacionPastaValidator : AbstractValidator<UpdatePreparacionPastaCommand>
{
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly IMediator _mediator;
    public UpdatePreparacionPastaValidator(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
        var scope = _serviceScopeFactory.CreateScope();
        _mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

        RuleFor(t => new CheckPreparacionPasteByNombreVariable
        {
            NombreVariable = t.NombreVariable,
            PreparacionPastaId = t.PreparacionPastaId
        }).NotEmpty().Must(BeUnique).WithMessage("Ya existe un registro con el mismo Nombre de Variable");
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
