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

[Route("Tag/CRUD")]
[ApiController]
public class TagsController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public TagsController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    [Authorize(Roles = "admin, author")]
    public ActionResult<Tags> CreateTag(Tags tag)
    {
        _context.Tags.Add(tag);
        _context.SaveChanges();
        return CreatedAtAction(nameof(GetTag), new { id = tag.Id }, tag);
    }

    [HttpGet]
    [Authorize(Roles = "admin")]
    public ActionResult<IEnumerable<Tags>> GetTags()
    {
        var tags = _context.Tags.ToList();
        return Ok(tags);
    }

    [HttpGet("{id}")]
    [Authorize(Roles = "admin")]
    public ActionResult<Tags> GetTag(int id)
    {
        var tag = _context.Tags.Find(id);
        if (tag == null)
        {
            return NotFound();
        }
        return Ok(tag);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "admin")]
    public IActionResult UpdateTag(int id, Tags tag)
    {
        if (id != tag.Id)
        {
            return BadRequest();
        }

        var existingTag = _context.Tags.Find(id);
        if (existingTag == null)
        {
            return NotFound();
        }

        existingTag.Name = tag.Name;

        _context.SaveChanges();

        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "admin")]
    public ActionResult<Tags> DeleteTag(int id)
    {
        var tag = _context.Tags.Find(id);

        if (tag == null)
        {
            return NotFound();
        }

        _context.Tags.Remove(tag);
        _context.SaveChanges();

        return tag;
    }
}

