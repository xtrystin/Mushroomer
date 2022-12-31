using MediatR;

namespace User.Application.Command;

public class AddUserCommand : IRequest
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string EmailAddress { get; set; }
}
