﻿using Post.Domain.ValueObject;

namespace Post.Domain.Entity;

public class Comment
{
    public Guid Id { get; private set; }

    private string _content;
    private DateTime _createdDate;
    private DateTime _lastModificationDate;
    //private Guid/User AuthorId
    
    public PostId PostId { get; private set; }
    public Post Post { get; private set; }

    public Comment(Guid id, string content, DateTime createdDate, PostId postId, Post post)
    {
        Id = id;
        _content = content;
        _createdDate = createdDate;
        PostId = postId;
        Post = post;
        _lastModificationDate = createdDate;
    }

    public Comment()
    {

    }

    public void ModifyContent(string content)
    {
        _content = content;
        _lastModificationDate = DateTime.Now;
    }
}
