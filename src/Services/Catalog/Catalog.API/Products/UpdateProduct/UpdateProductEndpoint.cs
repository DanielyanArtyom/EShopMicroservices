namespace CatalogAPI.Products.UpdateProduct;

public record UpdateProductRequest(
    Guid Id,
    string Name,
    List<string> Category,
    string Description,
    string ImageUrl,
    decimal Price);

public record UpdateProductResponse(bool IsSucces);

public class UpdateProductEndpoint: ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/products", async (UpdateProductRequest req, ISender sender) =>
            {
                var cmd = req.Adapt<UpdateProductCommand>();

                var result = await sender.Send(cmd);

                var response = result.Adapt<UpdateProductCommand>();

                return Results.Ok(response);
            })
            .WithName("UpdateProduct")
            .Produces<UpdateProductResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithSummary("Update Product")
            .WithDescription("Update Product");
    }
}