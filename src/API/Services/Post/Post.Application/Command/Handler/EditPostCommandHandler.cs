using Common.Const;
using Common.Helpers;
using MediatR;
using Post.Application.Exception;
using Post.Application.Service;
using Post.Domain.Repository;
using Post.Domain.ValueObject;

namespace Post.Application.Command.Handler;

public class EditPostCommandHandler : IRequestHandler<EditPostCommand>
{
    private readonly IPostRepository _postRepository;
    private readonly IAuthService _authService;

    public EditPostCommandHandler(IPostRepository postRepository, IAuthService authService)
    {
        _postRepository = postRepository;
        _authService = authService;
    }

    public async Task<Unit> Handle(EditPostCommand request, CancellationToken cancellationToken)
    {
        var post = await _postRepository.GetAsync(request.PostId);
        if (post is null)
        {
            throw new PostNotFoundException();
        }
        else if (post.IsAuthor(request.UserId) || await _authService.IsUserInRole(request.UserId, AuthUserRole.Moderator))
        {
            post.ChangeTitle(request.Title);
            post.ChangeContent(request.Content.Sanitize());
            post.ChangeThumbnailPhotoUrl(request.ThumbnailPhotoUrl);

            await _postRepository.UpdateAsync(post);
            return Unit.Value;
        }
        else
        {
            throw new NotAuthorizedToEditPost();
        }
    }
}
