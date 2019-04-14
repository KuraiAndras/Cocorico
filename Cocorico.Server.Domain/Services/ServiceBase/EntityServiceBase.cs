using Cocorico.Server.Domain.Extensions;
using Cocorico.Server.Domain.Models;
using Cocorico.Shared.Exceptions;
using Cocorico.Shared.Services.Helpers;
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

        protected async Task<IServiceResult> AddOrUpdateAsync(T entity)
        {
            var result = Context.GetDbSet<T>();

            switch (result)
            {
                case Success<DbSet<T>> success:
                    var dbSet = success.Data;

                    var original = await dbSet.SingleOrDefaultAsync(e => e.Id.Equals(entity.Id));

                    if (!(original is null)) dbSet.Remove(original);

                    entity.IsDeleted = false;

                    await dbSet.AddAsync(entity);

                    var saveResult = await Context.TrySaveChangesAsync();

                    return saveResult;

                default: return new Fail(new UnexpectedException());
            }
        }

        protected async Task<IServiceResult> DeleteAsync(TKey key)
        {
            var result = Context.GetDbSet<T>();

            switch (result)
            {
                case Success<DbSet<T>> success:
                    var dbSet = success.Data;

                    var original = await dbSet.SingleOrDefaultAsync(e => e.Id.Equals(key));

                    if (original is null) return new Fail(new EntityNotFoundException());

                    original.IsDeleted = true;

                    var saveResult = await Context.TrySaveChangesAsync();

                    return saveResult;

                default: return new Fail(new UnexpectedException());
            }
        }
    }
}
