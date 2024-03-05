using CocktailDev.Products.Api.Application.ViewModels;
using MediatR;

namespace CocktailDev.Products.Api.Application.Queries;

public record GetProductsQuery() : IRequest<List<ProductViewModel>>;
