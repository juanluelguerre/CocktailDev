using CocktailDev.Products.Api.Domain.Aggregates;
using MediatR;

namespace CocktailDev.Products.Api.Application.Commands;

public record CreateProductCommand(Guid Id, string Name, string Description, decimal Price)
    : IRequest<Product>;
