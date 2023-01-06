using MediatR;
using Post.Application.Exception;
using Post.Domain.Repository;

namespace Post.Application.Command.Handler;

public class DeletePostCommandHandler : IRequestHandler<DeletePostCommand>
{
    private readonly IPostRepository _postRepository;

    public DeletePostCommandHandler(IPostRepository postRepository)
    {
        _postRepository = postRepository;
    }
    public async Task<Unit> Handle(DeletePostCommand request, CancellationToken cancellationToken)
    {
        var post = await _postRepository.GetAsync(request.PostId); //todo check if null
        if (post.IsAuthor(request.UserId) is false)
        {
            throw new NotAuthorizedToEditPost();
        }
        
        await _postRepository.DeleteAsync(post);
        return Unit.Value;
    }
}
