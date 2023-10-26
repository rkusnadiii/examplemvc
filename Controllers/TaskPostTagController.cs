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


[Route("Posttag/CRUD")]
[ApiController]
public class PostTagsController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public PostTagsController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    [Authorize(Roles = "admin, author")]
    public ActionResult<PostTag> CreatePostTag(PostTag postTag)
    {
        _context.PostTags.Add(postTag);
        _context.SaveChanges();
        return CreatedAtAction(nameof(GetPostTag), new { id = postTag}, postTag);
    }

    [HttpGet]
    [Authorize(Roles = "admin")]
    public ActionResult<IEnumerable<PostTag>> GetPostTags()
    {
        var postTags = _context.PostTags.ToList();
        return Ok(postTags);
    }

    [HttpGet("{id}")]
    [Authorize(Roles = "admin")]
    public ActionResult<PostTag> GetPostTag(int id)
    {
        var postTag = _context.PostTags.Find(id);
        if (postTag == null)
        {
            return NotFound();
        }
        return Ok(postTag);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "admin")]
    public IActionResult UpdatePostTag(int id, PostTag postTag)
    {
        if (id != postTag.PostId || id !=postTag.TagId)
        {
            return BadRequest();
        }

        var existingPostTag = _context.PostTags.Find(id);
        if (existingPostTag == null)
        {
            return NotFound();
        }

        existingPostTag.PostId = postTag.PostId;
        existingPostTag.TagId = postTag.TagId;

        _context.SaveChanges();

        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "admin")]
    public ActionResult<PostTag> DeletePostTag(int id)
    {
        var postTag = _context.PostTags.Find(id);

        if (postTag == null)
        {
            return NotFound();
        }

        _context.PostTags.Remove(postTag);
        _context.SaveChanges();

        return postTag;
    }
}

