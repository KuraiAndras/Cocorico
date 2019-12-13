using AutoMapper;
using Cocorico.Persistence;
using Cocorico.Shared.Dtos.Opening;
using Cocorico.Shared.Exceptions;
using Cocorico.Shared.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cocorico.Server.Domain.Services.Opening
{
    public class OpeningService : IOpeningService
    {
        private readonly CocoricoDbContext _context;
        private readonly IMapper _mapper;

        public OpeningService(
            CocoricoDbContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ICollection<OpeningDto>> GetAllOpeningsAsync()
        {
            var openingsInDb = await _context.Openings
                .AsNoTracking()
                .Include(o => o.Orders)
                .AsNoTracking()
                .ToListAsync();

            return _mapper.Map<ICollection<OpeningDto>>(openingsInDb);
        }

        public async Task AddOpening(AddOpeningDto addOpeningDto)
        {
            if (!addOpeningDto.Start.HasValue) throw new ArgumentException(nameof(addOpeningDto));
            if (!addOpeningDto.End.HasValue) throw new ArgumentException(nameof(addOpeningDto));
            if (addOpeningDto.Start.Value.DateDifferenceTo(addOpeningDto.End.Value) != DateDifference.Sooner) throw new ArgumentException("Start is sooner than End", nameof(addOpeningDto));

            var opening = _mapper.Map<Cocorico.Domain.Entities.Opening>(addOpeningDto);

            var openingsInDb = await _context.Openings.AsNoTracking().ToListAsync();

            var lastOpening = openingsInDb.OrderByDescending(o => o.End).FirstOrDefault()
                              ?? new Cocorico.Domain.Entities.Opening { End = new DateTime(), Start = new DateTime() };

            if (lastOpening.End.DateDifferenceTo(opening.Start) != DateDifference.Sooner) throw new ArgumentException("New start is sooner than last end", nameof(addOpeningDto));

            await _context.Openings.AddAsync(opening);

            await _context.SaveChangesAsync();
        }

        public async Task UpdateOpening(OpeningDto openingDto)
        {
            if (!openingDto.Start.HasValue) throw new ArgumentException(nameof(openingDto));
            if (!openingDto.End.HasValue) throw new ArgumentException(nameof(openingDto));
            if (openingDto.Start.Value.DateDifferenceTo(openingDto.End.Value) != DateDifference.Sooner) throw new ArgumentException(nameof(openingDto));

            var opening = _mapper.Map<Cocorico.Domain.Entities.Opening>(openingDto);

            _context.Openings.Update(opening);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteOpening(int openingId)
        {
            var opening = await _context.Openings.AsNoTracking().SingleOrDefaultAsync(o => o.Id == openingId);

            if (opening is null) return;

            _context.Openings.Remove(opening);

            await _context.SaveChangesAsync();
        }

        public async Task<bool> CanAddOrderAsync(DateTime addTime)
        {
            var lastOpeningEnd = await _context.Openings
                .AsNoTracking()
                .OrderByDescending(o => o.End)
                .FirstOrDefaultAsync();

            if (lastOpeningEnd is null) throw new UnexpectedException("No Opening in Database");

            return addTime.DateDifferenceTo(lastOpeningEnd.End) == DateDifference.Sooner
                   && addTime.DateDifferenceTo(lastOpeningEnd.Start) != DateDifference.Sooner;
        }
    }
}