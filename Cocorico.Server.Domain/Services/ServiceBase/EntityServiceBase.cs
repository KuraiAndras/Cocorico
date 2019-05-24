using System;
using System.Collections.Generic;
using System.Linq;
using Cocorico.Server.Domain.Extensions;
using Cocorico.Server.Domain.Models;
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

            var _ = await dbSet.SingleOrDefaultAsync(e => e.Id.Equals(entity.Id))
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

    internal static class ManyToManyHelper
    {
        public static void TryUpdateManyToMany<T, TKey>(
            this DbContext db,
            IEnumerable<T> currentItems,
            IEnumerable<T> newItems,
            Func<T, TKey> getKey)
            where T : class
        {
            db.Set<T>().RemoveRange(currentItems.Except(newItems, getKey));
            db.Set<T>().AddRange(newItems.Except(currentItems, getKey));
        }

        public static IEnumerable<T> Except<T, TKey>(this IEnumerable<T> items, IEnumerable<T> other, Func<T, TKey> getKeyFunc)
        {
            return items
                .GroupJoin(other, getKeyFunc, getKeyFunc, (item, tempItems) => new { item, tempItems })
                .SelectMany(t => t.tempItems.DefaultIfEmpty(), (t, temp) => new { t, temp })
                .Where(t => ReferenceEquals(null, t.temp) || t.temp.Equals(default(T)))
                .Select(t => t.t.item);
        }
    }
}
