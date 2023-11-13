using CommandHandlers.Commands;
using CommandHandlers.Handler;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QueryHandlers.Handlers;
using QueryHandlers.Queries;

namespace Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/posts")]
    public class PostController : ControllerBase
    {
        private readonly CreatePostCommandHandler _createPostCommandHandler;
        private readonly GetPostQueryHandler _getPostQueryHandler;

        public PostController(CreatePostCommandHandler createPostCommandHandler, GetPostQueryHandler getPostQueryHandler)
        {
            _createPostCommandHandler = createPostCommandHandler;
            _getPostQueryHandler = getPostQueryHandler;
        }

        [HttpPost]
        public async Task<IActionResult> CreatePost([FromBody] CreatePostCommand command)
        {
            var postId = await _createPostCommandHandler.HandleAsync(command);
            return CreatedAtAction(nameof(GetPost), new { id = postId }, null);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPost(int id)
        {
            var post = await _getPostQueryHandler.HandleAsync(new GetPostQuery { Id = id });

            if (post == null)
            {
                return NotFound();
            }

            return Ok(post);
        }
    }
}
