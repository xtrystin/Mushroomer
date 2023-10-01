using MediatR;
using Post.Domain.Entity;
using Post.Domain.Repository;
using Post.Domain.ValueObject;
using Common.Helpers;

namespace Post.Application.Command.Handler;

public class AddPostCommandHandler : IRequestHandler<AddPostCommand>
{
    private readonly IPostRepository _postRepository;
    private readonly IUserRepository _userRepository;

    public AddPostCommandHandler(IPostRepository postRepository, IUserRepository userRepository)
    {
        _postRepository = postRepository;
        _userRepository = userRepository;
    }
    public async Task<Unit> Handle(AddPostCommand request, CancellationToken cancellationToken)
    {
        PostId postId = new(Guid.NewGuid());
        PostTitle postTitle = new(request.Title);
        PostContent postContent = new(request.Content.Sanitize());
        var user = await _userRepository.GetAsync(request.AuthorId);
        
        var post = new Domain.Entity.Post(postId, postTitle, postContent, null, user);   //todo: factory method
        if (request.AutoActivate)
            post.Activate();

        await _postRepository.AddAsync(post);
        return Unit.Value;
    }
}
