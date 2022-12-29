using MediatR;
using Post.Domain.Repository;
using Post.Domain.ValueObject;

namespace Post.Application.Command.Handler;

public class AddPostCommandHandler : IRequestHandler<AddPostCommand>
{
    private readonly IPostRepository _postRepository;

    public AddPostCommandHandler(IPostRepository postRepository)
    {
        _postRepository = postRepository;
    }
    public async Task<Unit> Handle(AddPostCommand request, CancellationToken cancellationToken)
    {
        PostId postId = new(Guid.NewGuid());
        PostTitle postTitle = new(request.Title);
        PostContent postContent= new(request.Content);

        var post = new Domain.Entity.Post(postId, postTitle, postContent, request.CreatedDate, null);
        await _postRepository.AddAsync(post);

        return Unit.Value;
    }
}
