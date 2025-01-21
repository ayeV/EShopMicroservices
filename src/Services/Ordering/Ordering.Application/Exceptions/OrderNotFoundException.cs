using BuildingBlocks.Exceptions;

namespace Ordering.Application.Exceptions
{
    [Serializable]
    public class OrderNotFoundException : NotFoundException
    {
       
        public OrderNotFoundException(Guid id) : base("Order",id)
        {
        }
    }
}