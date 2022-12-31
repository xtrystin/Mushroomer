using MediatR;
using Post.Domain.Entity;
using Post.Domain.Repository;

namespace Post.Application.Command.Handler;

public class AddCommentCommandHandler : IRequestHandler<AddCommentCommand>
{
    private readonly IPostRepository _postRepository;
    private readonly IUserRepository _userRepository;

    public AddCommentCommandHandler(IPostRepository postRepository, IUserRepository userRepository)
    {
        _postRepository = postRepository;
        _userRepository = userRepository;
    }

    public async Task<Unit> Handle(AddCommentCommand request, CancellationToken cancellationToken)
    {
        var post = await _postRepository.GetAsync(request.PostId);
        var user = await _userRepository.GetAsync(request.AuthorId);

        var comment = new Comment(default, request.Content, DateTime.Now, request.PostId, post, user);    //todo: Guid.NewGuid does not work in DB

        post.AddComment(comment);
        await _postRepository.UpdateAsync(post);

        return Unit.Value;
    }
}
