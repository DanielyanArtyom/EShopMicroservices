
using Ordering.Application.Orders.Queries.GetOrders;

namespace Ordering.API.Endpoints;

public record GetOrdersResponse(IEnumerable<OrderDto> Orders);

public class GetOrders: ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/orders", async ([AsParameters] PaginationRequest paging, ISender sender) =>
        {
            var result = await sender.Send(new GetOrdersQuery(paging));

            return Results.Ok(result.Adapt<GetOrdersResponse>());
        })
        .WithName("GetOrders")
        .Produces<GetOrdersByCustomerResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .WithSummary("Get Orders")
        .WithDescription("Get Orders");
    }
}