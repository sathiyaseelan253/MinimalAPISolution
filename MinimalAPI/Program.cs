using MinimalAPI.Models;
using MinimalAPI.RouteMapGroup;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGroup("/products").ProductsAPI();



app.Run();
