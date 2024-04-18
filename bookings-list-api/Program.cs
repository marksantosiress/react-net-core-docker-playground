using BookingsListDemoApi.Models;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

var bookings = new List<Booking>
{
    new Booking { Id = 1, Status = "Confirmed" },
    new Booking { Id = 2, Status = "Pending" },
    new Booking { Id = 3, Status = "Cancelled" }
};

app.MapGet("/bookings", () => bookings);

app.MapGet("/bookings/{id}", (int id) =>
{
    var booking = bookings.FirstOrDefault(b => b.Id == id);
    return booking is not null ? Results.Ok(booking) : Results.NotFound();
});

app.Run();