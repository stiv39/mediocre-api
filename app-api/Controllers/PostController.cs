using app_domain.Core;
using app_domain.Models;
using Microsoft.AspNetCore.Mvc;


namespace app_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public PostController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<ActionResult<IList<Post>>> GetAllPosts()
        {
            var posts = await _unitOfWork.Posts.GetAll();
            return Ok(posts);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Post>> GetPostById(int id)
        {
            var post = await _unitOfWork.Posts.GetById(id);
            if (post == null) return NotFound();

            return Ok(post);
        }

        [HttpPost]
        public async Task<ActionResult> CreatePost(string author, string title, string body)
        {
            _unitOfWork.Posts.Add(new Post { Author = author, Title = title, Body = body });
            await _unitOfWork.CompleteAsync();
            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> UpdatePost(Post post)
        {
            var postDb = await _unitOfWork.Posts.GetById(post.Id);
            if (postDb == null) return NotFound();

            _unitOfWork.Posts.Update(post);

            await _unitOfWork.CompleteAsync();

            return Ok();
        }

        [HttpDelete]
        public async Task<ActionResult> DeletePost(int id)
        {
            var postDb = await _unitOfWork.Posts.GetById(id);
            if (postDb == null) return NotFound();

            _unitOfWork.Posts.Delete(postDb);

            await _unitOfWork.CompleteAsync();

            return Ok();
        }
    }
}
