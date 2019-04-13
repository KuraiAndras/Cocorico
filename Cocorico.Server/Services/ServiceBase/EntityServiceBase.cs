using Cocorico.Server.Models;
using Cocorico.Shared.Services.Helpers;
using System;
using System.Threading.Tasks;
using Cocorico.Server.Extensions;
using Cocorico.Shared.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Cocorico.Server.Services.ServiceBase
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
        private readonly CocoricoDbContext _context;

        protected EntityServiceBase(CocoricoDbContext context) => _context = context;

        public async Task<IServiceResult> AddOrUpdateAsync(T entity)
        {
            var result = _context.GetDbSet<T>();

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
                        await _context.SaveChangesAsync();
                    }
                    catch (Exception)
                    {
                        return new Fail(new DatabaseException());
                    }

                    return new Success();

                default: return new Fail(new UnexpectedException());
            }
        }

        public async Task<IServiceResult> DeleteAsync(TKey key)
        {
            var result = _context.GetDbSet<T>();

            switch (result)
            {
                case Success<DbSet<T>> success:
                    var dbSet = success.Data;

                    var original = await dbSet.SingleOrDefaultAsync(e => e.Id.Equals(key));

                    if (original is null) return new Fail(new EntityNotFoundException());

                    original.IsDeleted = true;

                    try
                    {
                        await _context.SaveChangesAsync();
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
