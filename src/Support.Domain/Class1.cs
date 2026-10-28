namespace Support.Domain;

public enum TicketStatus
{
    New,
    InProgress,
    Closed
}

public class SupportTicket
{
    public Guid Id { get; private set; }
    public string Title { get; private set; }
    public TicketStatus Status { get; private set; }

    // Конструктор для создания новой заявки
    public SupportTicket(Guid id, string title)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Заголовок не может быть пустым.", nameof(title));

        Id = id;
        Title = title;
        Status = TicketStatus.New;
    }

    // Конструктор для восстановления из БД (или репозитория)
    private SupportTicket() { }

    // Бизнес-правило изменения статуса (находится в Domain!)
    public void ChangeStatus(TicketStatus newStatus)
    {
        if (Status == TicketStatus.Closed)
            throw new InvalidOperationException("Нельзя изменить статус закрытой заявки.");

        if (Status == TicketStatus.New && newStatus != TicketStatus.InProgress)
            throw new InvalidOperationException("Новую заявку можно только взять в работу (InProgress).");

        if (Status == TicketStatus.InProgress && newStatus != TicketStatus.Closed)
            throw new InvalidOperationException("Заявку в работе можно только закрыть (Closed).");

        Status = newStatus;
    }
}