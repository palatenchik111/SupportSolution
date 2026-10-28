using Support.Domain;

namespace Support.UnitTests;

public class SupportTicketTests
{
    [Fact]
    public void ChangeStatus_FromNewToInProgress_ShouldSucceed()
    {
        // Arrange
        var ticket = new SupportTicket(Guid.NewGuid(), "Тестовая заявка");

        // Act
        ticket.ChangeStatus(TicketStatus.InProgress);

        // Assert
        Assert.Equal(TicketStatus.InProgress, ticket.Status);
    }

    [Fact]
    public void ChangeStatus_FromClosedToNew_ShouldThrowException()
    {
        // Arrange
        var ticket = new SupportTicket(Guid.NewGuid(), "Тестовая заявка");
        ticket.ChangeStatus(TicketStatus.InProgress);
        ticket.ChangeStatus(TicketStatus.Closed);

        // Act & Assert
        var exception = Assert.Throws<InvalidOperationException>(() =>
            ticket.ChangeStatus(TicketStatus.New));

        Assert.Equal("Нельзя изменить статус закрытой заявки.", exception.Message);
    }
}