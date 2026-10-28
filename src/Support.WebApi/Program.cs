using Support.Application;
using Support.Domain;
using Support.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Регистрация зависимости: интерфейс из Application, реализация из Infrastructure
builder.Services.AddSingleton<ITicketRepository, InMemoryTicketRepository>();

var app = builder.Build();

// Endpoint: Создание заявки
app.MapPost("/tickets", async (string title, ITicketRepository repo, CancellationToken ct) =>
{
    var ticket = new SupportTicket(Guid.NewGuid(), title);
    await repo.SaveAsync(ticket, ct);
    return Results.Created($"/tickets/{ticket.Id}", ticket);
});

// Endpoint: Получение заявки и изменение статуса
app.MapPost("/tickets/{id:guid}/status", async (Guid id, TicketStatus newStatus, ITicketRepository repo, CancellationToken ct) =>
{
    var ticket = await repo.GetAsync(id, ct);
    if (ticket is null) return Results.NotFound();

    // HTTP-слой только передает данные, бизнес-правило сработает внутри ticket.ChangeStatus
    ticket.ChangeStatus(newStatus);
    await repo.SaveAsync(ticket, ct);

    return Results.Ok(ticket);
});

app.Run();