using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Cocorico.Server.Domain.Models;
using Cocorico.Shared.Exceptions;
using Cocorico.Shared.Services.Helpers;
using Microsoft.EntityFrameworkCore;

namespace Cocorico.Server.Domain.Extensions
{
    public static class CocoricoDbContextExtension
    {
        private static readonly Dictionary<Type, Func<CocoricoDbContext, object>> SetGetters = new Dictionary<Type, Func<CocoricoDbContext, object>>();
        private static readonly IEnumerable<PropertyInfo> Properties = typeof(CocoricoDbContext).GetProperties();

        public static IServiceResult<DbSet<T>> GetDbSet<T>(this CocoricoDbContext context) where T : class
        {
            var entityType = typeof(T);

            if (!SetGetters.ContainsKey(entityType))
            {
                var result = AddPropertyAccessor<T>();
                if (result is Fail) return new Fail<DbSet<T>>(new UnexpectedException());
            }

            return SetGetters[entityType](context) is DbSet<T> dbSet
                ? (IServiceResult<DbSet<T>>)new Success<DbSet<T>>(dbSet)
                : new Fail<DbSet<T>>(new UnexpectedException());
        }

        private static IServiceResult AddPropertyAccessor<T>() where T : class
        {
            var property = Properties.SingleOrDefault(prop => prop.PropertyType == typeof(DbSet<T>));

            if (property is null) return new Fail(new UnexpectedException());

            SetGetters[typeof(T)] = context => property.GetValue(context);

            return new Success();
        }
    }
}
