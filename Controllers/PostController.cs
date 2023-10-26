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
    public class PostController : Controller
    {
        private readonly ILogger<PostController> _logger;
        private readonly ApplicationDbContext _dbContext;

        public PostController(ILogger<PostController> logger, ApplicationDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        [HttpGet("/Home/Create")]
        public IActionResult InsertForm()
        {
            return View("/Views/CRUD/Create.cshtml");
        }

        [HttpPost("/Home/Create")]
        public IActionResult ProcessInsert([FromForm] InsertPostRequest data)
        {
          var res = ModelState
            .Select(s => s.Value.Errors)
            .Where(w => w.Count > 0)
            .ToList();
        
        if (ModelState.IsValid == false)
        {
            return View("/Views/Errors.cshtml", res);
        }
        
        var d = new Post() {
            Title = data.Title,
            Body = data.Body,
            CreatedAt = DateTime.Now
        };

        _dbContext.Post.Add(d);
        _dbContext.SaveChanges();

        TempData.Add("msg", "Berhasil add data");

        // return Ok(new {body = request});
        return RedirectToAction("Read","Home");
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
