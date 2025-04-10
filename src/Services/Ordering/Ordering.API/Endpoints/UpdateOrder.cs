
using Ordering.Application.Orders.Comands.UpdateOrder;

namespace Ordering.API.Endpoints;

public record UpdateOrderRequest(OrderDto Order);

public record UpdateOrderResponse(bool IsSuccess);

public class UpdateOrder: ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/orders", async (UpdateOrderRequest req, ISender sender) =>
        {
            var cmd = req.Adapt<UpdateOrderCommand>();

            var result = await sender.Send(cmd);

            return Results.Ok(result.Adapt<UpdateOrderResponse>());
        }).WithName("UpdateOrder")
        .Produces<UpdateOrderResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .WithSummary("Update Order")
        .WithDescription("Update Order");
    }
}