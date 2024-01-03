using MediatR;
using SocialMemin.Application.Core;
using SocialMemin.Domain;
using SocialMemin.Persistence;

namespace SocialMemin.Application.Activities
{
    public class Details
    {
        public class Query : IRequest<Result<Activity>>
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<Activity>>
        {
            private readonly DataContext _context;

            public Handler(DataContext context) => _context = context;

            public async Task<Result<Activity>> Handle(Query request, CancellationToken cancellationToken) => 
                Result<Activity>.Success(await _context.Activities.FindAsync(request.Id));
            
            
        }
    }
}
