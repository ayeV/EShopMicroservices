﻿
namespace Catalog.API.Products.GetProductById
{
    public record GetProductByIdQuery(Guid Id) : IQuery<GetProductByIdResult>;
    public record GetProductByIdResult(Product Product);
    public class GetProductByIdQueryHandler(IDocumentSession session) 
        : IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
    {
        public async Task<GetProductByIdResult> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
        {

            var product = await session.LoadAsync<Product>(query.Id,cancellationToken);

            return product == null ? throw new ProductNotFoundExeption(query.Id) : new GetProductByIdResult(product);
        }
    }
}
