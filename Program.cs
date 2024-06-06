using NET8_GlobalExceptionHandling;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddExceptionHandler<BadRequestExceptionHandler>()
    .AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

var app = builder.Build();
app.UseExceptionHandler();

app.MapGet("/", () => { throw new NotImplementedException(); });
app.MapGet("/{id}", (int id) => { if (id <= 0) throw new BadHttpRequestException("Invalid id"); });

app.Run();
