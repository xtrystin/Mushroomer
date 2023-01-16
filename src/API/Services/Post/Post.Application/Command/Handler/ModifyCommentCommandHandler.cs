using MediatR;
using Post.Application.Exception;
using Post.Domain.Repository;

namespace Post.Application.Command.Handler;

public class ModifyCommentCommandHandler : IRequestHandler<ModifyCommentCommand>
{
    private readonly IPostRepository _postRepository;

    public ModifyCommentCommandHandler(IPostRepository postRepository)
    {
        _postRepository = postRepository;
    }

    public async Task<Unit> Handle(ModifyCommentCommand request, CancellationToken cancellationToken)
    {
        var post = await _postRepository.GetAsync(request.PostId);
        //if (post.IsCommentAuthor(request.CommentId , request.UserId) is false)
        //{
        //    throw new NotAuthorizedToModifyComment();
        //}

        post.ModifyComment(request.CommentId, request.Content);
        await _postRepository.UpdateAsync(post);

        return Unit.Value;
    }
}
