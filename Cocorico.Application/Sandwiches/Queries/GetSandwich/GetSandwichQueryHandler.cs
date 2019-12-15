using AutoMapper;
using Cocorico.Application.Common.Persistence;
using Cocorico.Domain.Exceptions;
using Cocorico.Shared.Dtos.Sandwich;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Cocorico.Application.Sandwiches.Queries.GetSandwich
{
    public class GetSandwichQueryHandler : IRequestHandler<GetSandwichQuery, SandwichDto>
    {
        private readonly ICocoricoDbContext _context;
        private readonly IMapper _mapper;

        public GetSandwichQueryHandler(ICocoricoDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<SandwichDto> Handle(GetSandwichQuery request, CancellationToken cancellationToken)
        {
            var sandwich = await _context
                               .Sandwiches
                               .Include(s => s.SandwichIngredients)
                               .ThenInclude(il => il.Ingredient)
                               .SingleOrDefaultAsync(s => s.Id == request.Id, cancellationToken)
                           ?? throw new EntityNotFoundException($"Cant find sandwich with id:{request.Id}");

            return _mapper.Map<SandwichDto>(sandwich);
        }
    }
}
