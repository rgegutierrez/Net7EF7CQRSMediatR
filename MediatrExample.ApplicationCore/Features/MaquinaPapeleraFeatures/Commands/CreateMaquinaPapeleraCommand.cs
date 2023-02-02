using AutoMapper;
using FluentValidation;
using MediatR;
using MediatrExample.ApplicationCore.Domain;
using MediatrExample.ApplicationCore.Infrastructure.Persistence;

namespace MediatrExample.ApplicationCore.Features.MaquinaPapeleraFeatures.Commands;
public class CreateMaquinaPapeleraCommand : IRequest
{
    public int Orden { get; set; }
    public string NombreVariable { get; set; } = default!;
    public int LineaProduccion { get; set; }
    public string UnidadMedida { get; set; } = default!;
    public decimal? ValorMinimo { get; set; }
    public decimal? ValorMaximo { get; set; }
    public bool Obligatoria { get; set; }
    public bool ModoIngreso { get; set; }
    public string FormulaCalculo { get; set; } = default!;
    public bool Estado { get; set; }
    public List<VariableFormula>? Variables { get; set; }
}

public class CreateMaquinaPapeleraCommandHandler : IRequestHandler<CreateMaquinaPapeleraCommand>
{
    private readonly MyAppDbContext _context;
    private readonly IMapper _mapper;

    public CreateMaquinaPapeleraCommandHandler(MyAppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(CreateMaquinaPapeleraCommand request, CancellationToken cancellationToken)
    {
        if(request.ModoIngreso)
        {
            request.ValorMinimo = null; 
            request.ValorMaximo = null;
        }
        var crtObj = _mapper.Map<MaquinaPapelera>(request);

        try
        {
            crtObj.Orden = (from item in _context.MaquinasPapeleras select item.Orden).Max();
        }
        catch (Exception)
        {

        }

        crtObj.Orden += 1;
        List<VariableFormula> aux = new();

        foreach (var item in crtObj.Variables)
        {
            aux.Add(new VariableFormula { Letra = item.Letra, VariableId = item.VariableId});
        }

        _context.MaquinasPapeleras.Add(crtObj);
        await _context.SaveChangesAsync();

        foreach (VariableFormula item in crtObj.Variables)
        {
            item.MaquinaPapeleraId = crtObj.MaquinaPapeleraId;
            item.VariableId = aux.Where(o => o.Letra == item.Letra).First().VariableId;
        }
        await _context.SaveChangesAsync();

        return Unit.Value;
    }
}
public class CreateMaquinaPapeleraCommandMapper : Profile
{
    public CreateMaquinaPapeleraCommandMapper() =>
        CreateMap<CreateMaquinaPapeleraCommand, MaquinaPapelera>();
}

public class CreateMaquinaPapeleraValidator : AbstractValidator<CreateMaquinaPapeleraCommand>
{
    public CreateMaquinaPapeleraValidator()
    {
        RuleFor(r => r.NombreVariable).NotNull().MaximumLength(100);
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
            .Must(v => v.ValorMinimo <= v.ValorMaximo)
            .WithMessage("Valor Mínimo debe ser menor que Valor Máximo");
        RuleFor(r => r.Obligatoria).NotNull();
        RuleFor(r => r.Estado).NotNull();
    }
}
