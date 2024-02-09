using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text.Json.Serialization;

var st = Stopwatch.StartNew();

#region CreateSlimBuilder

var builder = WebApplication.CreateSlimBuilder(args);

#endregion

#region CreateEmptyBuilder

//var builder = WebApplication.CreateEmptyBuilder(new());
//builder.WebHost.UseKestrelCore();
//builder.Services.AddRoutingCore();

#endregion

// Add services to the container.
builder.Services.AddSingleton<IHelloService, HelloService>();

builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.TypeInfoResolverChain.Insert(0, AppJsonSerializerContext.Default);
});

var app = builder.Build();

app.Lifetime.ApplicationStarted.Register(() =>
{ 
    Console.WriteLine($"Application started: Elapsed: {st.Elapsed.TotalMilliseconds}");
});

var sampleTodos = new Todo[] {
    new(1, "Walk the dog"),
    new(2, "Do the dishes", DateOnly.FromDateTime(DateTime.Now)),
    new(3, "Do the laundry", DateOnly.FromDateTime(DateTime.Now.AddDays(1))),
    new(4, "Clean the bathroom"),
    new(5, "Clean the car", DateOnly.FromDateTime(DateTime.Now.AddDays(2)))
};

var todosApi = app.MapGroup("/todos");
todosApi.MapGet("/", () => sampleTodos);
todosApi.MapGet("/{id}", (int id) =>
    sampleTodos.FirstOrDefault(a => a.Id == id) is { } todo
        ? Results.Ok(todo)
        : Results.NotFound());

var sourceGeneratorApi = app.MapGroup("/salutes");
sourceGeneratorApi.MapGet("/{name}", (string name, [FromServices] IHelloService service) => service.Salute(name));

app.Run();

public record Todo(int Id, string? Title, DateOnly? DueBy = null, bool IsComplete = false);

[JsonSerializable(typeof(Todo[]))]
internal partial class AppJsonSerializerContext : JsonSerializerContext
{

}
public interface IHelloService
{
    public string Salute(string name);
}
public class HelloService : IHelloService
{
    public string Salute(string name) => $"Hello {name}!";
}