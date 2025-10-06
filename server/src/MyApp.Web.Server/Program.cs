var builder = WebApplication.CreateBuilder(args);

builder.Services.AddExceptionHandler<ExceptionHandlerMiddleware>();

var app = builder.Build();

app.UseHttpsRedirection();

app.UseDefaultFiles();
app.UseStaticFiles();

app.MapGet("/api/me", () => new
{
    firstName = "John",
    lastName = "Doe",
});

app.MapFallbackToFile("index.html", new StaticFileOptions()
{
    OnPrepareResponse = context =>
    {
        var responseHeaders = context.Context.Response.Headers;
        responseHeaders.CacheControl = "no-cache, no-store, must-revalidate";
        responseHeaders.Pragma = "no-cache";
        responseHeaders.Expires = "0";
    }
});

app.UseExceptionHandler(_ => {});

await app.RunAsync();
