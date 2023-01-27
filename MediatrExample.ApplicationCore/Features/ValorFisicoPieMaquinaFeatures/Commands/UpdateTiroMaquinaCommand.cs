using AutoMapper;
using FluentValidation;
using MediatR;
using MediatrExample.ApplicationCore.Common.Helpers;
using MediatrExample.ApplicationCore.Domain;
using MediatrExample.ApplicationCore.Infrastructure.Persistence;

namespace MediatrExample.ApplicationCore.Features.ValorFisicoPieMaquinaFeatures.Commands;
public class UpdateValorFisicoPieMaquinaCommand : IRequest
{
    public string ValorFisicoPieMaquinaId { get; set; }
    public string UnidadMedida { get; set; } = default!;
    public decimal ValorMinimo { get; set; }
    public decimal ValorMaximo { get; set; }
    public bool Obligatoria { get; set; }
    public bool Estado { get; set; }
}

public class UpdateValorFisicoPieMaquinaCommandHandler : IRequestHandler<UpdateValorFisicoPieMaquinaCommand>
{
    private readonly MyAppDbContext _context;
    private readonly IMapper _mapper;

    public UpdateValorFisicoPieMaquinaCommandHandler(MyAppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(UpdateValorFisicoPieMaquinaCommand request, CancellationToken cancellationToken)
    {
        var updObj = _mapper.Map<ValorFisicoPieMaquina>(request);

        var oObj = _context.ValoresFisicoPieMaquina.Find(updObj.ValorFisicoPieMaquinaId);
        oObj.UnidadMedida = updObj.UnidadMedida;
        oObj.ValorMinimo = updObj.ValorMinimo;
        oObj.ValorMaximo = updObj.ValorMaximo;
        oObj.Obligatoria = updObj.Obligatoria;
        oObj.Estado = updObj.Estado;

        await _context.SaveChangesAsync();

        return Unit.Value;
    }
}
public class UpdateValorFisicoPieMaquinaCommandMapper : Profile
{
    public UpdateValorFisicoPieMaquinaCommandMapper() =>
        CreateMap<UpdateValorFisicoPieMaquinaCommand, ValorFisicoPieMaquina>()
            .ForMember(dest =>
                dest.ValorFisicoPieMaquinaId,
                opt => opt.MapFrom(mf => mf.ValorFisicoPieMaquinaId.FromHashId()));
}

public class UpdateValorFisicoPieMaquinaValidator : AbstractValidator<UpdateValorFisicoPieMaquinaCommand>
{
    public UpdateValorFisicoPieMaquinaValidator()
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
