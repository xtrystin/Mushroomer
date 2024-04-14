using MediatR;
using Post.Application.Exception;
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
        if (post is null)
            throw new PostNotFoundException();

        var user = await _userRepository.GetAsync(request.UserId);
        if (user is null)
            throw new UserNotFoundException();

        if (request.Like is true)
            post.Like(user);
        else
            post.DisLike(user);

        await _postRepository.UpdateAsync(post);
        return Unit.Value;
    }
}
