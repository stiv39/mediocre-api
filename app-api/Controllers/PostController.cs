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
        private static List<Post> posts = new List<Post>
            {
                new Post { Id = 1, Author = "Luis Enrique", Title = "Hello world", Body="Hello world!"},
                new Post { Id = 2, Author = "Felipe Scolari", Title = "Ola friends", Body="Lorem impsum"},
                new Post { Id = 3, Author = "Hugo Chavez", Title = "Not like this", Body="Lorem impsum Lorem impsum Lorem impsum"},
                new Post { Id = 4, Author = "Diego Costa", Title = "What the hell", Body="Lorem impsum Lorem impsumLorem impsumLorem impsum"}
            };


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
