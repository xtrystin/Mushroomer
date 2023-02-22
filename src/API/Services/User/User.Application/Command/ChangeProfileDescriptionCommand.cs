using MediatR;

namespace User.Application.Command;

public class ChangeProfileDescriptionCommand : IRequest
{
    public Guid UserId { get; set; }
    public string ProfileDescription { get; set; }
    public Guid ActionAuthorId { get; set; }        //todo: move to class ActionContext?
    public string ActionAuthorRole { get; set; }

}
