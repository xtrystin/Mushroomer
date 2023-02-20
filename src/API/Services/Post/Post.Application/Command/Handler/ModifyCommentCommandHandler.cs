using Common.Const;
using MediatR;
using Post.Application.Exception;
using Post.Application.Service;
using Post.Domain.Repository;

namespace Post.Application.Command.Handler;

public class ModifyCommentCommandHandler : IRequestHandler<ModifyCommentCommand>
{
    private readonly IPostRepository _postRepository;
    private readonly IAuthService _authService;

    public ModifyCommentCommandHandler(IPostRepository postRepository, IAuthService authService)
    {
        _postRepository = postRepository;
        _authService = authService;
    }

    public async Task<Unit> Handle(ModifyCommentCommand request, CancellationToken cancellationToken)
    {
        var post = await _postRepository.GetAsync(request.PostId);
        if (post is null)
        {
            throw new PostNotFoundException();
        }
        else if (post.IsCommentAuthor(request.CommentId, request.UserId) || await _authService.IsUserInRole(request.UserId, AuthUserRole.Moderator))
        {
            post.ModifyComment(request.CommentId, request.Content);
            await _postRepository.UpdateAsync(post);

            return Unit.Value;
        }
        else
        {
            throw new NotAuthorizedToModifyComment();
        }
    }
}
