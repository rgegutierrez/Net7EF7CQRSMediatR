using AutoMapper;
using FluentValidation;
using MediatR;
using MediatrExample.ApplicationCore.Common.Helpers;
using MediatrExample.ApplicationCore.Domain;
using MediatrExample.ApplicationCore.Infrastructure.Persistence;

namespace MediatrExample.ApplicationCore.Features.MaquinaPapeleraFeatures.Commands;

public class UpdateOrdenCommand : IRequest
{
    public List<OrdenMaquinaPapelera>? OrdenLista { get; set; }
}

public class UpdateOrdenCommandHandler : IRequestHandler<UpdateOrdenCommand>
{
    private readonly MyAppDbContext _context;
    private readonly IMapper _mapper;

    public UpdateOrdenCommandHandler(MyAppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(UpdateOrdenCommand request, CancellationToken cancellationToken)
    {
        var index = 1;
        foreach (var item in request.OrdenLista)
        {
            var maquinaPapelera = _mapper.Map<MaquinaPapelera>(item);
            var oObj = _context.MaquinasPapeleras.Find(maquinaPapelera.MaquinaPapeleraId);
            oObj.Orden = index++;
        }

        await _context.SaveChangesAsync();

        return Unit.Value;
    }
}
public class UpdateOrdenCommandMapper : Profile
{
    public UpdateOrdenCommandMapper() =>
        CreateMap<OrdenMaquinaPapelera, MaquinaPapelera>()
            .ForMember(dest =>
                        dest.MaquinaPapeleraId,
                        opt => opt.MapFrom(mf => mf.MaquinaPapeleraId.FromHashId()));
}

public class UpdateOrdenValidator : AbstractValidator<UpdateOrdenCommand>
{
    public UpdateOrdenValidator()
    {

    }
}

public class OrdenMaquinaPapelera
{
    public string MaquinaPapeleraId { get; set; }
    public int Orden { get; set; }
}