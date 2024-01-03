using MediatR;
using Microsoft.EntityFrameworkCore;
using SocialMemin.Application.Core;
using SocialMemin.Domain;
using SocialMemin.Persistence;

namespace SocialMemin.Application.Activities
{
    public class List
    {
        public class Query : IRequest<Result<List<Activity>>> { }

        public class Handler : IRequestHandler<Query, Result<List<Activity>>>
        {
            private readonly DataContext _context;

            public Handler(DataContext context) =>_context = context;

            public async Task<Result<List<Activity>>> Handle(Query request, CancellationToken cancellationToken) => 
                Result<List<Activity>>.Success(await _context.Activities.ToListAsync());

        }
    }
}
