using AutoMapper;
using Cocorico.Application.Common.Persistence;
using Cocorico.Shared.Dtos.Sandwich;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Cocorico.Application.Sandwiches.Queries.GetAllSandwich
{
    public sealed class GetAllSandwichQueryHandler : IRequestHandler<GetAllSandwichQuery, ICollection<SandwichDto>>
    {
        private readonly ICocoricoDbContext _context;
        private readonly IMapper _mapper;

        public GetAllSandwichQueryHandler(
            ICocoricoDbContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ICollection<SandwichDto>> Handle(GetAllSandwichQuery request, CancellationToken cancellationToken)
        {
            var sandwiches = await _context
                .Sandwiches
                .Include(s => s.SandwichIngredients)
                .ThenInclude(il => il.Ingredient)
                .ToListAsync(cancellationToken);

            return sandwiches.Select(s => _mapper.Map<SandwichDto>(s)).ToList();
        }
    }
}
