namespace CatalogAPI.Products.DeleteProduct;

public record DeleteProductRequest(Guid Id): ICommand<DeleteProductResponse>;

public record DeleteProductResponse(bool IsSuccess);

public class DeleteProductEndpoint: ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/products/{id}", async (Guid id,ISender sender) =>
            {
                var cmd = await sender.Send(new DeleteProductRequest(id));

                var response = cmd.Adapt<DeleteProductResponse>();

                return Results.Ok(response);
            })
            .WithName("DeleteProduct")
            .Produces<DeleteProductResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithSummary("Delete Product")
            .WithDescription("Delete Product");
    }
}