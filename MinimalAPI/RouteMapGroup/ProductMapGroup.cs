using MinimalAPI.Models;
using System.Text.Json;

namespace MinimalAPI.RouteMapGroup
{
    public static class ProductMapGroup
    {
        static List<Product> products = new List<Product>()
        {
            new Product() { ProductID = 1, ProductName="LG TV" },
            new Product() { ProductID=2, ProductName="iPad"}
        };

        public static RouteGroupBuilder ProductsAPI(this RouteGroupBuilder group)
        {
            //Get all products
            group.MapGet("/", async (HttpContext context) =>
            {
                var availableProducts = products.Select(product => product.ToString());
                await context.Response.WriteAsync(JsonSerializer.Serialize(availableProducts));
            });

            //Get a single product by Product ID, with constraint
            group.MapGet("/{id:int}", (HttpContext context, int id) =>
            {
                Product? product = products.FirstOrDefault(product => product.ProductID == id);
                if (product == null)
                {
                    //context.Response.StatusCode = 400;
                    //await context.Response.WriteAsync("Incorrect Product ID");
                    //return;
                    return Results.BadRequest(new { errorMessageFromGet = "Incorrect Product ID" });
                }
                return Results.Ok(product);
                //await context.Response.WriteAsync(JsonSerializer.Serialize(product));
            });
            //Adds a new product
            group.MapPost("/", (HttpContext context, Product product) =>
            {
                products.Add(product);
                //await context.Response.WriteAsync("New product added");
                return Results.Ok(new { messageFromPost = "New product added" });
            });
            group.MapPut("/{id:int}", (HttpContext context,int id, Product updatedProduct) =>
            {
                Product? product = products.FirstOrDefault(product => product.ProductID == id);
                if (product == null)
                {
                    return Results.BadRequest(new { errorMessageFromUpdate = "Update operation failed" });
                    //context.Response.StatusCode = 400;
                    //await context.Response.WriteAsync("Update operation failed");
                    //return;
                }
                product.ProductName = updatedProduct.ProductName;
                //products.Add(productToUpdate);
                return Results.Ok(new { successMessageFromUpd = "Product updated successfully" });
                //await context.Response.WriteAsync("Product updated successfully");
            });
            group.MapDelete("/{id:int}", (HttpContext context, int id) =>
            {
                Product? product = products.FirstOrDefault(p => p.ProductID == id);
                if(product == null)
                {
                    return Results.BadRequest(new { errorMessageFromDelete = "Delete failed" });
                    //context.Response.StatusCode = 400;
                    //await context.Response.WriteAsync("Delete failed");
                    //return;
                }
                products.Remove(product);
                return Results.Ok(new { successMessage = "Product deleted successfully" });
                //await context.Response.WriteAsync("Product deleted successfully");
            });
            return group;
        }
    }
}
