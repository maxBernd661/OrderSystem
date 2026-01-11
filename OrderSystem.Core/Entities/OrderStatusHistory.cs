namespace OrderSystem.Core.Entities
{
    public class OrderStatusHistory : PersistentEntityBase
    {
        //für EF
        private OrderStatusHistory()
        { }

        public Guid OrderId { get; set; }

        public Order Order { get; set; }

        public OrderStatus ChangedTo { get; private set; }

        public OrderStatus? ChangedFrom { get; private set; }

        public static OrderStatusHistory Created(Order order)
        {
            return new()
            {
                Order = order,
                OrderId = order.Id,
                ChangedTo = OrderStatus.Draft
            };
        }

        public static OrderStatusHistory Changed(Order order, OrderStatus oldStatus, OrderStatus newStatus)
        {
            return new OrderStatusHistory()
            {
                Order = order,
                OrderId = order.Id,
                ChangedTo = newStatus,
                ChangedFrom = oldStatus
            };
        }
    }

    public sealed class OrderStatusHistoryQueryProfile : IQueryProfile<OrderStatusHistory>
    {
        public IQueryable<OrderStatusHistory> Apply(IQueryable<OrderStatusHistory> query)
        {
            return query;
        }
    }
}