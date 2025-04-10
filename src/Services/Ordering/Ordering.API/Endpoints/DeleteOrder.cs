using Ordering.Application.Orders.Comands.DeleteOrder;

namespace Ordering.API.Endpoints;

public record DeleteOrderResponse(bool IsSuccess);

public class DeleteOrder: ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/orders/{id}", async (Guid id, ISender sender) =>
        {
            var res = await sender.Send(new DeleteOrderCommand(id));

            return Results.Ok(res.Adapt<DeleteOrderResponse>());
        })
        .WithName("DeleteOrder")
        .Produces<DeleteOrderResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .WithSummary("Delete Order")
        .WithDescription("Delete Order");
    }
}