using BuildingBlocks.Exceptions;

namespace Basket.API.Exceptions
{
    public class BasketNotFounException : NotFoundException
    {
        public BasketNotFounException(string userName) : base("Basket",userName)
        {
        }
    }
}