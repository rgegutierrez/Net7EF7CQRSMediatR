using AutoMapper;
using FluentValidation;
using MediatR;
using MediatrExample.ApplicationCore.Common.Helpers;
using MediatrExample.ApplicationCore.Domain;
using MediatrExample.ApplicationCore.Infrastructure.Persistence;

namespace MediatrExample.ApplicationCore.Features.TiroMaquinaFeatures.Commands;
public class UpdateTiroMaquinaCommand : IRequest
{
    public string TiroMaquinaId { get; set; }
    public string UnidadMedida { get; set; } = default!;
    public decimal ValorMinimo { get; set; }
    public decimal ValorMaximo { get; set; }
    public bool Obligatoria { get; set; }
    public bool Estado { get; set; }
}

public class UpdateTiroMaquinaCommandHandler : IRequestHandler<UpdateTiroMaquinaCommand>
{
    private readonly MyAppDbContext _context;
    private readonly IMapper _mapper;

    public UpdateTiroMaquinaCommandHandler(MyAppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(UpdateTiroMaquinaCommand request, CancellationToken cancellationToken)
    {
        var updObj = _mapper.Map<TiroMaquina>(request);

        var oObj = _context.TirosMaquina.Find(updObj.TiroMaquinaId);
        oObj.UnidadMedida = updObj.UnidadMedida;
        oObj.ValorMinimo = updObj.ValorMinimo;
        oObj.ValorMaximo = updObj.ValorMaximo;
        oObj.Obligatoria = updObj.Obligatoria;
        oObj.Estado = updObj.Estado;

        await _context.SaveChangesAsync();

        return Unit.Value;
    }
}
public class UpdateTiroMaquinaCommandMapper : Profile
{
    public UpdateTiroMaquinaCommandMapper() =>
        CreateMap<UpdateTiroMaquinaCommand, TiroMaquina>()
            .ForMember(dest =>
                dest.TiroMaquinaId,
                opt => opt.MapFrom(mf => mf.TiroMaquinaId.FromHashId()));
}

public class UpdateTiroMaquinaValidator : AbstractValidator<UpdateTiroMaquinaCommand>
{
    public UpdateTiroMaquinaValidator()
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
