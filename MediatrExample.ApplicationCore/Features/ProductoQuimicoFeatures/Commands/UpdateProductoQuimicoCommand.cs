using AutoMapper;
using FluentValidation;
using MediatR;
using MediatrExample.ApplicationCore.Common.Helpers;
using MediatrExample.ApplicationCore.Domain;
using MediatrExample.ApplicationCore.Infrastructure.Persistence;

namespace MediatrExample.ApplicationCore.Features.ProductoQuimicoFeatures.Commands;
public class UpdateProductoQuimicoCommand : IRequest
{
    public string ProductoQuimicoId { get; set; }
    public string UnidadMedida { get; set; } = default!;
    public decimal ValorMinimo { get; set; }
    public decimal ValorMaximo { get; set; }
    public string Funcion { get; set; } = default!;
    public bool Certificacion { get; set; }
    public bool Obligatoria { get; set; }
    public bool Estado { get; set; }
}

public class UpdateProductoQuimicoCommandHandler : IRequestHandler<UpdateProductoQuimicoCommand>
{
    private readonly MyAppDbContext _context;
    private readonly IMapper _mapper;

    public UpdateProductoQuimicoCommandHandler(MyAppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(UpdateProductoQuimicoCommand request, CancellationToken cancellationToken)
    {
        var updObj = _mapper.Map<ProductoQuimico>(request);

        var oObj = _context.ProductosQuimicos.Find(updObj.ProductoQuimicoId);
        oObj.UnidadMedida = updObj.UnidadMedida;
        oObj.ValorMinimo = updObj.ValorMinimo;
        oObj.ValorMaximo = updObj.ValorMaximo;
        oObj.Certificacion = updObj.Certificacion;
        oObj.Funcion = updObj.Funcion;
        oObj.Obligatoria = updObj.Obligatoria;
        oObj.Estado = updObj.Estado;

        await _context.SaveChangesAsync();

        return Unit.Value;
    }
}
public class UpdateProductoQuimicoCommandMapper : Profile
{
    public UpdateProductoQuimicoCommandMapper() =>
        CreateMap<UpdateProductoQuimicoCommand, ProductoQuimico>()
            .ForMember(dest =>
                dest.ProductoQuimicoId,
                opt => opt.MapFrom(mf => mf.ProductoQuimicoId.FromHashId()));
}

public class UpdateProductoQuimicoValidator : AbstractValidator<UpdateProductoQuimicoCommand>
{
    public UpdateProductoQuimicoValidator()
    {
        RuleFor(r => r.UnidadMedida).NotNull();
        RuleFor(r => r.ValorMinimo).NotNull().GreaterThanOrEqualTo(0).LessThanOrEqualTo(100);
        RuleFor(r => r.ValorMaximo).NotNull().GreaterThanOrEqualTo(0).LessThanOrEqualTo(100);
        RuleFor(r => r.Obligatoria).NotNull();
        RuleFor(r => r.Estado).NotNull();
    }
}
