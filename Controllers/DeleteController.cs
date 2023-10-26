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
    public class DeleteController : Controller
    {
        private readonly ILogger<PostController> _logger;
        private readonly ApplicationDbContext _dbContext;

        public DeleteController(ILogger<PostController> logger, ApplicationDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        [HttpGet("/Home/Delete/{id}")]
        public IActionResult DeletePost(int id)
        {
            var post = _dbContext.Post.FirstOrDefault(p => p.Id == id);
            if (post == null)
            {
                return NotFound("Post not found");
            }

            return View("/Views/CRUD/Delete.cshtml", post);
        }

        [HttpPost("/Home/Delete/{id}")]
        public IActionResult DeletePostConfirmed(int id)
        {
            try
            {
                var post = _dbContext.Post.FirstOrDefault(p => p.Id == id);
                if (post == null)
                {
                    return NotFound("Post not found");
                }

                _dbContext.Post.Remove(post);
                _dbContext.SaveChanges();

                DisplaySuccessMessage("Post deleted successfully!");
                return RedirectToAction("HomePost");
            }
            catch (Exception ex)
            {
                DisplayErrorMessage($"Failed to delete post: {ex.Message}");
                return RedirectToAction("DeletePost", new { id });
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