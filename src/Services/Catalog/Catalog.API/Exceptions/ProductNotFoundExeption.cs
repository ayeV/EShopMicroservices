using BuildingBlocks.Exceptions;

namespace Catalog.API.Exceptions
{
    internal class ProductNotFoundExeption : NotFoundException
    {
       
        public ProductNotFoundExeption(Guid Id) : base("Product",Id)
        {
        }

    }
}