using Ordering.Application.Orders.Queries.GetOrdersByCustomer;

namespace Ordering.API.Endpoints;

public record GetOrdersByCustomerResponse(IEnumerable<OrderDto> Orders);

public class GetOrdersByCustomer: ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/orders/customer/{customerId}", async (Guid customerId, ISender sender) =>
            {
                var result = await sender.Send(new GetOrdersByCustomerQuery(customerId));

                return Results.Ok(result.Adapt<GetOrdersByCustomerResponse>());
            })
            .WithName("GetOrdersByCustomer")
            .Produces<GetOrdersByCustomerResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithSummary("Get Orders By Customer")
            .WithDescription("Get Orders By Customer");
    }
}