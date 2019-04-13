using Cocorico.Server.Api.Extensions;
using Cocorico.Server.Api.Models;
using Cocorico.Shared.Exceptions;
using Cocorico.Shared.Services.Helpers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Cocorico.Server.Api.Services.ServiceBase
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

                    try
                    {
                        await Context.SaveChangesAsync();
                    }
                    catch (Exception)
                    {
                        return new Fail(new DatabaseException());
                    }

                    return new Success();

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

                    try
                    {
                        await Context.SaveChangesAsync();
                    }
                    catch (Exception)
                    {
                        return new Fail(new DatabaseException());
                    }

                    return new Success();

                default: return new Fail(new UnexpectedException());
            }
        }
    }
}
