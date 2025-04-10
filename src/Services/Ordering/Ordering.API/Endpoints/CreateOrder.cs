
using Carter;
using Mapster;
using MediatR;
using Ordering.Application.Orders.Comands.CreateOrder;

namespace Ordering.API.Endpoints;

public record CreateOrderRequest(OrderDto Order);

public record CreateOrderResponse(Guid Id);

public class CreateOrder: ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/orders", async (CreateOrderRequest req, ISender sender) =>
        {
            var cmd = req.Adapt<CreateOrderCommand>();

            var result = await sender.Send(cmd);
            var res = result.Adapt<CreateOrderResponse>();

            return Results.Created($"/orders/{res.Id}", res);
        })
        .WithName("CreateOrder")
        .Produces<CreateOrderResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .WithSummary("Create Order")
        .WithDescription("Create Order");
    }
}