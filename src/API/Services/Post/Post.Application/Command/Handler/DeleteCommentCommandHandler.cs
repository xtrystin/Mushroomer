using MediatR;
using Post.Application.Exception;
using Post.Domain.Repository;

namespace Post.Application.Command.Handler;

public class DeleteCommentCommandHandler : IRequestHandler<DeleteCommentCommand>
{
    private readonly IPostRepository _postRepository;

    public DeleteCommentCommandHandler(IPostRepository postRepository)
    {
        _postRepository = postRepository;
    }

    public async Task<Unit> Handle(DeleteCommentCommand request, CancellationToken cancellationToken)
    {
        var post = await _postRepository.GetAsync(request.PostId);  //todo: check if user is author
        if (post == null) 
        {
            throw new PostNotFoundException();
        }

        post.RemoveComment(request.CommentId);
        _postRepository.UpdateAsync(post);

        return Unit.Value;
    }
}