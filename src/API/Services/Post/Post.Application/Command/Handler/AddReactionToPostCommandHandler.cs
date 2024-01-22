using MediatR;
using Post.Domain.Repository;

namespace Post.Application.Command.Handler;

public class AddReactionToPostCommandHandler : IRequestHandler<AddReactionToPostCommand>
{
    private readonly IPostRepository _postRepository;
    private readonly IUserRepository _userRepository;

    public AddReactionToPostCommandHandler(IPostRepository postRepository, IUserRepository userRepository)
    {
        _postRepository = postRepository;
        _userRepository = userRepository;
    }
    public async Task<Unit> Handle(AddReactionToPostCommand request, CancellationToken cancellationToken)
    {
        var post = await _postRepository.GetAsync(request.PostId);
        var user = await _userRepository.GetAsync(request.UserId);
        
        if (request.Like is true)
            post.Like(user);
        else
            post.DisLike(user);

        await _postRepository.UpdateAsync(post);
        return Unit.Value;
    }
}
