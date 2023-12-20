using MediatR;
using SocialMemin.Domain;
using SocialMemin.Persistence;

namespace SocialMemin.Application.Activities
{
    public class Create
    {
        public class ActivityRequest : IRequest
        {
            public Activity Activity { get; set; }
        }

        public class Handler : IRequestHandler<ActivityRequest>
        {
            private readonly DataContext _context;

            public Handler(DataContext context) => _context = context;

            async Task IRequestHandler<ActivityRequest>.Handle(ActivityRequest request, CancellationToken cancellationToken)
            {
                _context.Activities.Add(request.Activity);
                await _context.SaveChangesAsync();
            }
        }
    }
}
