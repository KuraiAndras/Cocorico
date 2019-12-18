using Cocorico.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Cocorico.Application.Common.Persistence
{
    public interface ICocoricoDbContext
    {
        DbSet<Sandwich> Sandwiches { get; set; }
        DbSet<Order> Orders { get; set; }
        DbSet<Ingredient> Ingredients { get; set; }
        DbSet<Opening> Openings { get; set; }
        DbSet<CocoricoUser> Users { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
