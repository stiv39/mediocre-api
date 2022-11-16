using app_data.Context;
using app_domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore;

namespace app_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly DataContext _context;
        public PostController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IList<Post>>> GetAllPosts()
        {
            var posts = await _context.Posts.ToListAsync();
            return Ok(posts);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Post>> GetPostById(int id)
        {
            var post = await _context.Posts.FirstOrDefaultAsync(post => post.Id == id);
            if (post == null) return NotFound();

            return Ok(post);
        }

        [HttpPost]
        public async Task<ActionResult> CreatePost(string author, string title, string body)
        {
            await _context.Posts.AddAsync(new Post { Author = author, Title = title, Body = body });
            _context.SaveChanges();
            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> UpdatePost(Post post)
        {
            var dbPost = await _context.Posts.FindAsync(post.Id);
            if (dbPost == null) return BadRequest();

            dbPost.Author = post.Author;
            dbPost.Title = post.Title;
            dbPost.Body = post.Body;

            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete]
        public async Task<ActionResult> DeletePost(int id)
        {
            var post = await _context.Posts.FirstOrDefaultAsync(post => post.Id == id);
            if (post == null) return BadRequest();

            _context.Posts.Remove(post);

            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
