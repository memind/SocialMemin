using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SocialMemin.Application.Core;
using SocialMemin.Domain;
using SocialMemin.Persistence;

namespace SocialMemin.Application.Activities
{
    public class Details
    {
        public class Query : IRequest<Result<ActivityDto>>
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<ActivityDto>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;

            public Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<Result<ActivityDto>> Handle(Query request, CancellationToken cancellationToken) => 
                Result<ActivityDto>.Success(await _context.Activities.ProjectTo<ActivityDto>(_mapper.ConfigurationProvider).FirstOrDefaultAsync(a => a.Id == request.Id));
            
            
        }
    }
}
