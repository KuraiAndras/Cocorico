using AutoMapper;
using Cocorico.Application.Common.Persistence;
using Cocorico.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Cocorico.Application.Sandwiches.Commands.AddSandwich
{
    public sealed class AddSandwichCommandHandler : AsyncRequestHandler<AddSandwichCommand>
    {
        private readonly IMapper _mapper;
        private readonly ICocoricoDbContext _context;

        public AddSandwichCommandHandler(IMapper mapper, ICocoricoDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        protected override async Task Handle(AddSandwichCommand request, CancellationToken cancellationToken)
        {
            var sandwich = _mapper.Map<Sandwich>(request.SandwichAddDto);

            var ingredients = await _context
                .Ingredients
                .ToListAsync(cancellationToken);

            sandwich.SandwichIngredients = ingredients
                .Where(i => request.SandwichAddDto.Ingredients.Any(iDto => iDto.Id == i.Id))
                .Select(i => new SandwichIngredient
                {
                    Ingredient = i,
                    Sandwich = sandwich,
                })
                .ToList();

            _context.Sandwiches.Add(sandwich);

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
