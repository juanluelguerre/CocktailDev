using CocktailDev.Products.Api.Application.ViewModels;
using MediatR;

namespace CocktailDev.Products.Api.Application.Queries;

public record GetProductQuery(long Id) : IRequest<ProductDetailViewModel>;
