using Common.Const;
using MediatR;
using Post.Application.Exception;
using Post.Application.Service;
using Post.Domain.Repository;

namespace Post.Application.Command.Handler;

public class DeletePostCommandHandler : IRequestHandler<DeletePostCommand>
{
    private readonly IPostRepository _postRepository;
    private readonly IAuthService _authService;

    public DeletePostCommandHandler(IPostRepository postRepository, IAuthService authService)
    {
        _postRepository = postRepository;
        _authService = authService;
    }
    public async Task<Unit> Handle(DeletePostCommand request, CancellationToken cancellationToken)
    {
        var post = await _postRepository.GetAsync(request.PostId);
        if (post is null)
        {
            throw new PostNotFoundException();
        }
        else if (post.IsAuthor(request.UserId) || await _authService.IsUserInRole(request.UserId, AuthUserRole.Moderator))
        {
            await _postRepository.DeleteAsync(post);
            return Unit.Value;
        }
        else
        {
            throw new NotAuthorizedToDeletePost();
        }
    }
}
