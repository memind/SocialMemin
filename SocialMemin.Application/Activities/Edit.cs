using MediatR;
using SocialMemin.Domain;
using SocialMemin.Persistence;

namespace SocialMemin.Application.Activities
{
    public class Edit
    {
        public class Command : IRequest
        {
            public Activity Activity { get; set; }
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly DataContext _context;

            public Handler(DataContext context) => _context = context;
            
            async Task IRequestHandler<Command>.Handle(Command request, CancellationToken cancellationToken)
            {
                request.Activity.Id = request.Id;
                var activity = await _context.Activities.FindAsync(request.Activity.Id);

                await _context.SaveChangesAsync();
            }
        }
    }
}
