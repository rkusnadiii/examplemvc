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
using examplemvc.Controllers;

namespace examplemvc.Controllers
{

    [TypeFilter(typeof(CustomAuthorizeFilter))]
    public class DetailController : Controller
    {
        private readonly ILogger<PostController> _logger;
        private readonly ApplicationDbContext _dbContext;

        public DetailController(ILogger<PostController> logger, ApplicationDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }
        
        [HttpGet("/Home/Read")]
        public IActionResult HomePost()
        {
            var posts = _dbContext.Post.ToList();
            return View("/Views/CRUD/AllPost.cshtml", posts);
        }

        [HttpGet("/Home/Read/{id}")]
        public IActionResult ReadPost(int id)
        {
            var post = _dbContext.Post.FirstOrDefault(p => p.Id == id);
            if (post == null)
            {
                return NotFound("Post not found");
            }

            return View("/Views/CRUD/Read.cshtml", post);
        }
}

}