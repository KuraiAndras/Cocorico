using Cocorico.DAL.Extensions;
using Cocorico.DAL.Models;
using Cocorico.Shared.Exceptions;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Cocorico.Server.Domain.Services.ServiceBase
{
    public abstract class EntityServiceBase<T> : EntityServiceBase<T, int> where T : class, IDbEntity<int>
    {
        protected EntityServiceBase(CocoricoDbContext context)
            : base(context)
        {
        }
    }

    public abstract class EntityServiceBase<T, TKey> where T : class, IDbEntity<TKey>
    {
        protected readonly CocoricoDbContext Context;

        protected EntityServiceBase(CocoricoDbContext context) => Context = context;

        protected async Task AddAsync(T entity)
        {
            if (!entity.Id.Equals(default(TKey))) throw new InvalidCommandException();

            var dbSet = Context.GetDbSet<T>();

            if (!(await dbSet.AsNoTracking().SingleOrDefaultAsync(e => e.Id.Equals(entity.Id)) is null)) throw new EntityAlreadyExistsException();

            await dbSet.AddAsync(entity);

            await Context.SaveChangesAsync();
        }

        protected async Task UpdateAsync(T entity)
        {
            var dbSet = Context.GetDbSet<T>();

            var _ = await dbSet.AsNoTracking().SingleOrDefaultAsync(e => e.Id.Equals(entity.Id))
                    ?? throw new EntityNotFoundException();

            dbSet.Update(entity);

            await Context.SaveChangesAsync();
        }

        protected async Task DeleteByIdAsync(TKey key)
        {
            var dbSet = Context.GetDbSet<T>();

            var original = await dbSet.SingleOrDefaultAsync(e => e.Id.Equals(key)) ?? throw new EntityNotFoundException();

            original.IsDeleted = true;

            await Context.SaveChangesAsync();
        }
    }
}
