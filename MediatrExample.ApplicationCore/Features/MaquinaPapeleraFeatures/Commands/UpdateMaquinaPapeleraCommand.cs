using AutoMapper;
using FluentValidation;
using MediatR;
using MediatrExample.ApplicationCore.Common.Helpers;
using MediatrExample.ApplicationCore.Domain;
using MediatrExample.ApplicationCore.Infrastructure.Persistence;

namespace MediatrExample.ApplicationCore.Features.MaquinaPapeleraFeatures.Commands;
public class UpdateMaquinaPapeleraCommand : IRequest
{
    public string MaquinaPapeleraId { get; set; } = default!;
    public int Orden { get; set; }
    public string NombreVariable { get; set; } = default!;
    public int LineaProduccion { get; set; }
    public string UnidadMedida { get; set; } = default!;
    public decimal ValorMinimo { get; set; }
    public decimal ValorMaximo { get; set; }
    public bool Obligatoria { get; set; }
    public bool ModoIngreso { get; set; }
    public string FormulaCalculo { get; set; } = default!;
    public bool Estado { get; set; }
    public List<VariableFormula>? Variables { get; set; }
}

public class UpdateMaquinaPapeleraCommandHandler : IRequestHandler<UpdateMaquinaPapeleraCommand>
{
    private readonly MyAppDbContext _context;
    private readonly IMapper _mapper;

    public UpdateMaquinaPapeleraCommandHandler(MyAppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(UpdateMaquinaPapeleraCommand request, CancellationToken cancellationToken)
    {
        var updObj = _mapper.Map<MaquinaPapelera>(request);

        var oObj = _context.MaquinasPapeleras.Find(updObj.MaquinaPapeleraId);
        oObj.NombreVariable = updObj.NombreVariable;
        oObj.LineaProduccion = updObj.LineaProduccion;
        oObj.UnidadMedida = updObj.UnidadMedida;
        oObj.ValorMinimo = updObj.ValorMinimo;
        oObj.ValorMaximo = updObj.ValorMaximo;
        oObj.Obligatoria = updObj.Obligatoria;
        oObj.ModoIngreso = updObj.ModoIngreso;
        oObj.FormulaCalculo = updObj.FormulaCalculo;

        if (updObj.Estado != oObj.Estado)
        {
            if (updObj.Estado == false)
            {
                oObj.Orden = 0;
            }
            else
            {
                try
                {
                    oObj.Orden = (from item in _context.MaquinasPapeleras select item.Orden).Max();
                    oObj.Orden += 1;
                }
                catch (Exception)
                {

                }
            }
            oObj.Estado = updObj.Estado;
        }

        foreach (var item in _context.VariablesFormula.Where(o => o.MaquinaPapeleraId == updObj.MaquinaPapeleraId).ToList())
        {
            _context.VariablesFormula.Remove(item);
        }

        List<VariableFormula> aux = new List<VariableFormula>();

        foreach (var item in updObj.Variables)
        {
            aux.Add(new VariableFormula { Letra = item.Letra, VariableId = item.VariableId });
        }
         
        await _context.SaveChangesAsync();

        foreach (VariableFormula item in updObj.Variables)
        {
            item.MaquinaPapeleraId = updObj.MaquinaPapeleraId;
            item.VariableId = aux.Where(o => o.Letra == item.Letra).First().VariableId;
            _context.VariablesFormula.Add(item);
        }
        await _context.SaveChangesAsync();

        return Unit.Value;
    }
}
public class UpdateMaquinaPapeleraCommandMapper : Profile
{
    public UpdateMaquinaPapeleraCommandMapper() =>
        CreateMap<UpdateMaquinaPapeleraCommand, MaquinaPapelera>()
            .ForMember(dest =>
                dest.MaquinaPapeleraId,
                opt => opt.MapFrom(mf => mf.MaquinaPapeleraId.FromHashId()));
}

public class UpdateMaquinaPapeleraValidator : AbstractValidator<UpdateMaquinaPapeleraCommand>
{
    public UpdateMaquinaPapeleraValidator()
    {
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
}
