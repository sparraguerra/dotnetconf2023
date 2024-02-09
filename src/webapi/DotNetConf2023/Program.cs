using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

var st = Stopwatch.StartNew();

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<IHelloService, HelloService>();

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

public interface  IHelloService
{
    public string Salute(string name);
}
public class HelloService : IHelloService
{
    public string Salute(string name) => $"Hello {name}!";  
}