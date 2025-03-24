namespace CatalogAPI.Products.CreateProduct;

public record CreateProductRequest(string Name,
    List<string> Category,
    string Description,
    string ImageUrl,
    decimal Price);

public record CreateProductResponse(Guid Id);

public class CreateProductEndpoint: ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/products", async (CreateProductRequest req, ISender sender) =>
            {
                var cmd = req.Adapt<CreateProductCommand>();

                var result = await sender.Send(cmd);

                var response = result.Adapt<CreateProductResponse>();

                return Results.Created($"/products/{response.Id}", response);
            })
            .WithName("CreateProduct")
            .Produces<CreateProductResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithSummary("Create Product")
            .WithDescription("Create Product");
    }
}