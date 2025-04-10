

namespace Ordering.Application.Orders.EventHandlers;

public class OrderDeletedEventHandler(ILogger<OrderDeletedEventHandler> logger): INotificationHandler<OrderCreatedEvent>
{
    public Task Handle(OrderCreatedEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("Event OrderDeletedEventHandler working");
        
        return Task.CompletedTask;
        ;
    }
}