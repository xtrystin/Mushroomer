using MediatR;
using Post.Application.Exception;
using Post.Domain.Repository;

namespace Post.Application.Command.Handler;

public class EditPostCommandHandler : IRequestHandler<EditPostCommand>
{
    private readonly IPostRepository _postRepository;

    public EditPostCommandHandler(IPostRepository postRepository)
    {
        _postRepository = postRepository;
    }

    public async Task<Unit> Handle(EditPostCommand request, CancellationToken cancellationToken)
    {
        var post = await _postRepository.GetAsync(request.PostId);
        if (post.IsAuthor(request.UserId) is false)
        {
            throw new NotAuthorizedToEditPost();
        }

        post.ChangeTitle(request.Title);
        post.ChangeContent(request.Content);

        await _postRepository.UpdateAsync(post);
        return Unit.Value;
    }
}
