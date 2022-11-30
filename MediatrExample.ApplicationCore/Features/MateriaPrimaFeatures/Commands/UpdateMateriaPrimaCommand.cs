using AutoMapper;
using FluentValidation;
using MediatR;
using MediatrExample.ApplicationCore.Common.Helpers;
using MediatrExample.ApplicationCore.Domain;
using MediatrExample.ApplicationCore.Infrastructure.Persistence;

namespace MediatrExample.ApplicationCore.Features.MateriaPrimaFeatures.Commands;
public class UpdateMateriaPrimaCommand : IRequest
{
    public string MateriaPrimaId { get; set; }
    public string UnidadMedida { get; set; } = default!;
    public int ValorMinimo { get; set; }
    public int ValorMaximo { get; set; }
    public bool Obligatoria { get; set; }
    public bool Estado { get; set; }
}

public class UpdateMateriaPrimaCommandHandler : IRequestHandler<UpdateMateriaPrimaCommand>
{
    private readonly MyAppDbContext _context;
    private readonly IMapper _mapper;

    public UpdateMateriaPrimaCommandHandler(MyAppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(UpdateMateriaPrimaCommand request, CancellationToken cancellationToken)
    {
        var updObj = _mapper.Map<MateriaPrima>(request);

        var oObj = _context.MateriasPrimas.Find(updObj.MateriaPrimaId);
        oObj.UnidadMedida = updObj.UnidadMedida;
        oObj.ValorMinimo = updObj.ValorMinimo;
        oObj.ValorMaximo = updObj.ValorMaximo;
        oObj.Obligatoria = updObj.Obligatoria;
        oObj.Estado = updObj.Estado;

        await _context.SaveChangesAsync();

        return Unit.Value;
    }
}
public class UpdateMateriaPrimaCommandMapper : Profile
{
    public UpdateMateriaPrimaCommandMapper() =>
        CreateMap<UpdateMateriaPrimaCommand, MateriaPrima>()
            .ForMember(dest =>
                dest.MateriaPrimaId,
                opt => opt.MapFrom(mf => mf.MateriaPrimaId.FromHashId()));
}

public class UpdateMateriaPrimaValidator : AbstractValidator<UpdateMateriaPrimaCommand>
{
    public UpdateMateriaPrimaValidator()
    {
        RuleFor(r => r.UnidadMedida).NotNull();
        RuleFor(r => r.ValorMinimo).NotNull().GreaterThanOrEqualTo(0).LessThanOrEqualTo(100);
        RuleFor(r => r.ValorMaximo).NotNull().GreaterThanOrEqualTo(0).LessThanOrEqualTo(100);
        RuleFor(r => r.Obligatoria).NotNull();
        RuleFor(r => r.Estado).NotNull();
    }
}
