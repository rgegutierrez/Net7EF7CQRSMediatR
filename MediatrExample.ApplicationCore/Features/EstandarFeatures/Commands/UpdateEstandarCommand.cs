using AutoMapper;
using FluentValidation;
using MediatR;
using MediatrExample.ApplicationCore.Common.Helpers;
using MediatrExample.ApplicationCore.Domain;
using MediatrExample.ApplicationCore.Infrastructure.Persistence;

namespace MediatrExample.ApplicationCore.Features.EstandarFeatures.Commands;
public class UpdateEstandarCommand : IRequest
{
    public string EstandarId { get; set; } = default!;
    public string ClienteId { get; set; }
    public string TipoPapelId { get; set; }
    public int ValorFisicoPieMaquinaId { get; set; }
    public decimal ValorMinimo { get; set; }
    public decimal ValorPromedio { get; set; }
    public decimal? ValorMaximo { get; set; }
}

public class UpdateEstandarCommandHandler : IRequestHandler<UpdateEstandarCommand>
{
    private readonly MyAppDbContext _context;
    private readonly IMapper _mapper;

    public UpdateEstandarCommandHandler(MyAppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(UpdateEstandarCommand request, CancellationToken cancellationToken)
    {
        var updObj = _mapper.Map<Estandar>(request);

        var oObj = _context.Estandares.Find(updObj.EstandarId);
        oObj.ClienteId = updObj.ClienteId;
        oObj.TipoPapelId = updObj.TipoPapelId;
        oObj.ValorFisicoPieMaquinaId = updObj.ValorFisicoPieMaquinaId;
        oObj.ValorMinimo = updObj.ValorMinimo;
        oObj.ValorPromedio = updObj.ValorPromedio;
        oObj.ValorMaximo = updObj.ValorMaximo;

        await _context.SaveChangesAsync();

        return Unit.Value;
    }
}
public class UpdateEstandarCommandMapper : Profile
{
    public UpdateEstandarCommandMapper() =>
        CreateMap<UpdateEstandarCommand, Estandar>()
            .ForMember(dest =>
                dest.EstandarId,
                opt => opt.MapFrom(mf => mf.EstandarId.FromHashId()));
}

public class UpdateEstandarValidator : AbstractValidator<UpdateEstandarCommand>
{
    public UpdateEstandarValidator()
    {
        RuleFor(r => r.ClienteId).NotNull();
        RuleFor(r => r.TipoPapelId).NotNull();
        RuleFor(r => r.ValorFisicoPieMaquinaId).NotNull();
        RuleFor(r => r.ValorMinimo).NotNull();
        RuleFor(r => r.ValorPromedio).NotNull();
    }
}
