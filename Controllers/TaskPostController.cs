using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using examplemvc.Models;
using examplemvc.Data;

[Route("Post/CRUD")]
[ApiController]
public class PostsController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public PostsController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    [Authorize(Roles = "admin, author")]
    public ActionResult<Post> CreatePost(Post post)
    {
        _context.Post.Add(post);
        _context.SaveChanges();
        return CreatedAtAction(nameof(GetPost), new { id = post.Id }, post);
    }

    [HttpGet]
    [Authorize(Roles = "admin")]
    public ActionResult<IEnumerable<Post>> GetPosts()
    {
        var posts = _context.Post.ToList();
        return Ok(posts);
    }

    [HttpGet("{id}")]
    [Authorize(Roles = "admin")]
    public ActionResult<Post> GetPost(int id)
    {
        var post = _context.Post.Find(id);
        if (post == null)
        {
            return NotFound();
        }
        return Ok(post);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "admin")]
    public IActionResult UpdatePost(int id, Post post)
    {
        if (id != post.Id)
        {
            return BadRequest();
        }

        var existingPost = _context.Post.Find(id);
        if (existingPost == null)
        {
            return NotFound();
        }

        existingPost.Title = post.Title;
        existingPost.Body = post.Body;

        _context.SaveChanges();

        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "admin")]
    public ActionResult<Post> DeletePost(int id)
    {
        var post = _context.Post.Find(id);

        if (post == null)
        {
            return NotFound();
        }

        _context.Post.Remove(post);
        _context.SaveChanges();

        return post;
    }
}
