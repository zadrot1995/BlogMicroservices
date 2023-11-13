using Domain.Models;
using Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;
using QueryHandlers.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryHandlers.Handlers
{
    public class GetPostQueryHandler
    {
        private readonly AppDbContext _dbContext;

        public GetPostQueryHandler(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<BlogPost> HandleAsync(GetPostQuery query)
        {
            return await _dbContext.BlogPosts
                .Include(p => p.Comments)
                .FirstOrDefaultAsync(p => p.Id == query.Id);
        }
    }
}
