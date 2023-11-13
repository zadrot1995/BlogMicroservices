using CommandHandlers.Commands;
using Domain.Models;
using Infrastructure.DbContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandHandlers.Handler
{
    public class CreatePostCommandHandler
    {
        private readonly AppDbContext _dbContext;

        public CreatePostCommandHandler(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> HandleAsync(CreatePostCommand command)
        {
            var post = new BlogPost
            {
                Title = command.Title,
                Content = command.Content
            };

            _dbContext.BlogPosts.Add(post);
            await _dbContext.SaveChangesAsync();

            return post.Id;
        }
    }
}
