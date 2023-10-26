using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using examplemvc.Models.Request;
using Microsoft.AspNetCore.Authorization;
using examplemvc.Models;
using System;
using System.Linq;
using examplemvc.Filters;
using examplemvc.Data;


namespace examplemvc.Controllers
{
    [TypeFilter(typeof(CustomAuthorizeFilter))]
    public class UpdateController : Controller
    {
        private readonly ILogger<PostController> _logger;
        private readonly ApplicationDbContext _dbContext;

        public UpdateController(ILogger<PostController> logger, ApplicationDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

         [HttpGet("/Home/Update/{id}")]
        public IActionResult UpdatePost(int id)
        {
            var post = _dbContext.Post.FirstOrDefault(p => p.Id == id);
            if (post == null)
            {
                return NotFound("Post not found");
            }

            return View("/Views/CRUD/Update.cshtml", post);
        }

        [HttpPost("/Home/Update/{id}")]
        public IActionResult UpdatePost(int id, [FromForm] InsertPostRequest data)
        {
            try
            {
                var post = _dbContext.Post.FirstOrDefault(p => p.Id == id);
                if (post == null)
                {
                    return NotFound("Post not found");
                }

                post.Title = data.Title;
                post.Body = data.Body;

                _dbContext.SaveChanges();

                DisplaySuccessMessage("Post updated successfully!");
                return RedirectToAction("ReadPost", new { id = post.Id });
            }
            catch (Exception ex)
            {
                DisplayErrorMessage($"Failed to update post: {ex.Message}");
                return RedirectToAction("UpdatePost", new { id });
            }
        }
        public void DisplaySuccessMessage(string message)
        {
            TempData["SuccessMessage"] = message;
        }

        public void DisplayErrorMessage(string message)
        {
            TempData["ErrorMessage"] = message;
        }
}

}