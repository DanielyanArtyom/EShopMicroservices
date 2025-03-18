namespace CatalogAPI.Products.GetProducts;

public record GetProductRequests(int? pageNumber = 1, int? pageSize = 10);

public record GetProductsResponse(IEnumerable<Product> Products);

public class GetProductsEndpoint: ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products", async ([AsParameters] GetProductRequests requests, ISender sender) =>
            {
                var query = requests.Adapt<GetProductsQuery>();
                
                var result = await sender.Send(query);

                var response = result.Adapt<GetProductsResponse>();

                return Results.Ok(response);
            })
            .WithName("GetProducts")
            .Produces<GetProductsResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithSummary("Get Product")
            .WithDescription("Get Product");
    }
}