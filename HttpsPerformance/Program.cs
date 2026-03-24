using System.Net;
using System.Text.Json.Serialization;

namespace HttpsPerformance
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateSlimBuilder(args);

            builder.Services.ConfigureHttpJsonOptions(options =>
            {
                options.SerializerOptions.TypeInfoResolverChain.Insert(0, AppJsonSerializerContext.Default);
            });
            builder.WebHost.ConfigureKestrel(serverOptions =>
            {
                serverOptions.Listen(IPAddress.Any, 6001, listenOptions =>
                {
                    listenOptions.UseHttps("localhost.pfx", "yyz");
                });
                serverOptions.Listen(IPAddress.Any, 6002);
            });
            var app = builder.Build();

            var sampleTodos = new Todo[] {
                new(1, "Voltage:110KV"),
                new(2, "Current:200A", DateOnly.FromDateTime(DateTime.Now)),
                new(3, "Temperature:30¡æ", DateOnly.FromDateTime(DateTime.Now.AddDays(1))),
                new(4, "Oxygen:21.9%vol"),
                new(5, "Methane:0%LEL", DateOnly.FromDateTime(DateTime.Now.AddDays(2)))
            };

            var todosApi = app.MapGroup("/todos");
            todosApi.MapGet("/", () => sampleTodos);
            todosApi.MapGet("/{id}", (int id) =>
                sampleTodos.FirstOrDefault(a => a.Id == id) is { } todo
                    ? Results.Ok(todo)
                    : Results.NotFound());

            app.Run();
        }
    }

    public record Todo(int Id, string? Title, DateOnly? DueBy = null, bool IsComplete = false);

    [JsonSerializable(typeof(Todo[]))]
    internal partial class AppJsonSerializerContext : JsonSerializerContext
    {

    }
}
