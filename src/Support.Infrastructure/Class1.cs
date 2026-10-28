using Support.Application;
using Support.Domain;
using System.Collections.Concurrent;

namespace Support.Infrastructure;

public class InMemoryTicketRepository : ITicketRepository
{
    private readonly ConcurrentDictionary<Guid, SupportTicket> _store = new();

    public Task<SupportTicket?> GetAsync(Guid id, CancellationToken cancellationToken)
    {
        _store.TryGetValue(id, out var ticket);
        return Task.FromResult(ticket);
    }

    public Task SaveAsync(SupportTicket ticket, CancellationToken cancellationToken)
    {
        _store.AddOrUpdate(ticket.Id, ticket, (_, _) => ticket);
        return Task.CompletedTask;
    }
}