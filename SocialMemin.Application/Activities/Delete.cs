using MediatR;
using SocialMemin.Persistence;

namespace SocialMemin.Application.Activities
{
    public class Delete
    {
        public class Command : IRequest
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly DataContext _context;

            public Handler(DataContext context) => _context = context;
            
            async Task IRequestHandler<Command>.Handle(Command request, CancellationToken cancellationToken)
            {
                var activity = await _context.Activities.FindAsync(request.Id);
                _context.Remove(activity);

                await _context.SaveChangesAsync();
            }
        }
    }
}
