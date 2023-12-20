using MediatR;
using Microsoft.EntityFrameworkCore;
using SocialMemin.Domain;
using SocialMemin.Persistence;

namespace SocialMemin.Application.Activities
{
    public class List
    {
        public class Query : IRequest<List<Activity>> { }

        public class Handler : IRequestHandler<Query, List<Activity>>
        {
            private readonly DataContext _context;

            public Handler(DataContext context) =>_context = context;
            
            public async Task<List<Activity>> Handle(Query request, CancellationToken token) =>  await _context.Activities.ToListAsync();
            
        }
    }
}
