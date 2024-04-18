var builder = WebApplication.CreateBuilder(args);

// Add CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("bookinglist_frontend_policy", policy =>
    {
        options.AddPolicy("AllowAnyOrigin",
            builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
    });
});

// Add HTTP client services
builder.Services.AddHttpClient("BookingClient", client =>
{
    client.BaseAddress = new Uri("http://bookingapi-service/"); // Adjust the port accordingly
});

var app = builder.Build();

app.UseCors("AllowAnyOrigin");

app.MapGet("/bookings", async (IHttpClientFactory clientFactory) =>
{
    var client = clientFactory.CreateClient("BookingClient");
    var result = await client.GetAsync("bookings");
    result.EnsureSuccessStatusCode();
    return await result.Content.ReadAsStringAsync();
});

app.Run();
