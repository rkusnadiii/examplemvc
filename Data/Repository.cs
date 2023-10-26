using System;
using System.Collections.Generic;
using System.Linq;
using examplemvc.Models;
using Microsoft.EntityFrameworkCore;


namespace examplemvc.Data;

public class PostRepository : IPostRepository
{
    private readonly ApplicationDbContext _context;

    public PostRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Post>> GetPostsAsync()
    {
        return await _context.Post.ToListAsync();
    }

    public async Task<Post> GetPostAsync(int id)
    {
        return await _context.Post.FindAsync(id);
    }

    public void AddPost(Post post)
    {
        _context.Post.Add(post);
    }

    public void UpdatePost(Post post)
    {
        _context.Entry(post).State = EntityState.Modified;
    }

    public void DeletePost(Post post)
    {
        _context.Post.Remove(post);
    }
}

public interface IPostRepository
{
    Task<IEnumerable<Post>> GetPostsAsync();
    Task<Post> GetPostAsync(int id);
    void AddPost(Post post);
    void UpdatePost(Post post);
    void DeletePost(Post post);
}