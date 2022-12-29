using MediatR;
using Post.Domain.Entity;
using Post.Domain.Repository;

namespace Post.Application.Command.Handler;

public class AddCommentCommandHandler : IRequestHandler<AddCommentCommand>
{
    private readonly IPostRepository _postRepository;

    public AddCommentCommandHandler(IPostRepository postRepository)
    {
        _postRepository = postRepository;
    }

    public async Task<Unit> Handle(AddCommentCommand request, CancellationToken cancellationToken)
    {
        var post = await _postRepository.GetAsync(request.PostId);
        var comment = new Comment(default, request.Content, DateTime.Now, request.PostId, post);    //todo: Guid.NewGuid does not work in DB

        post.AddComment(comment);
        await _postRepository.UpdateAsync(post);

        return Unit.Value;
    }
}
