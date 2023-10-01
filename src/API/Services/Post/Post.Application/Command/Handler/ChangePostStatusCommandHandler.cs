using MediatR;
using Post.Application.Exception;
using Post.Domain.Repository;

namespace Post.Application.Command.Handler;
public class ChangePostStatusCommandHandler : IRequestHandler<ChangePostStatusCommand>
{
    private readonly IPostRepository _postRepository;

    public ChangePostStatusCommandHandler(IPostRepository postRepository)
    {
        _postRepository = postRepository;
    }

    public async Task<Unit> Handle(ChangePostStatusCommand request, CancellationToken cancellationToken)
    {
        var post = await _postRepository.GetAsync(request.PostId);
        if (post is null)
            throw new PostNotFoundException();

        if (request.ChangeToActive)
            post.Activate();
        else
            post.Deactivate();
        
        await _postRepository.UpdateAsync(post);
        return Unit.Value;
    }
}
