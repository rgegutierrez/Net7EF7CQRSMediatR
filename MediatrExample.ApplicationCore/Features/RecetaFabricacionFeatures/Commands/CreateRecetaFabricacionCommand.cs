using AutoMapper;
using FluentValidation;
using MediatR;
using MediatrExample.ApplicationCore.Common.Helpers;
using MediatrExample.ApplicationCore.Domain;
using MediatrExample.ApplicationCore.Infrastructure.Persistence;

namespace MediatrExample.ApplicationCore.Features.Products.Commands;
public class CreateRecetaFabricacionCommand : IRequest
{
    public string RecetaFabricacionId { get; set; }
    public List<RecetaMateriaPrima> RecetaMateriaPrima { get; set; }
}

public class CreateRecetaFabricacionCommandHandler : IRequestHandler<CreateRecetaFabricacionCommand>
{
    private readonly MyAppDbContext _context;
    private readonly IMapper _mapper;

    public CreateRecetaFabricacionCommandHandler(MyAppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(CreateRecetaFabricacionCommand request, CancellationToken cancellationToken)
    {
        foreach (var item in request.RecetaMateriaPrima)
        {
            item.RecetaFabricacionId = request.RecetaFabricacionId.FromHashId();
            _context.RecetasMateriaPrima.Add(item);
        }

        await _context.SaveChangesAsync();

        return Unit.Value;
    }
}
public class CreateRecetaFabricacionCommandMapper : Profile
{
    public CreateRecetaFabricacionCommandMapper() =>
        CreateMap<CreateRecetaFabricacionCommand, RecetaFabricacion>();
}

public class CreateRecetaFabricacionValidator : AbstractValidator<CreateRecetaFabricacionCommand>
{
    public CreateRecetaFabricacionValidator()
    {

    }
}
