using CocktailDev.Products.Api.Domain;
using MediatR;

namespace CocktailDev.Products.Api.Application.Commands;

public record CreateProductCommand(long Id, string Name, string Description, decimal Price)
    : IRequest<ProductDetail>;
