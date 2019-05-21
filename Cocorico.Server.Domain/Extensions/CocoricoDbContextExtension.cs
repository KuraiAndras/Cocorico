using Cocorico.Server.Domain.Models;
using Cocorico.Shared.Exceptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Cocorico.Server.Domain.Extensions
{
    public static class CocoricoDbContextExtension
    {
        private static readonly Dictionary<Type, Func<CocoricoDbContext, object>> SetGetters = new Dictionary<Type, Func<CocoricoDbContext, object>>();
        private static readonly IEnumerable<PropertyInfo> Properties = typeof(CocoricoDbContext).GetProperties();

        public static DbSet<T> GetDbSet<T>(this CocoricoDbContext context) where T : class
        {
            var entityType = typeof(T);

            if (!SetGetters.ContainsKey(entityType)) AddPropertyAccessor<T>();

            return SetGetters[entityType](context) as DbSet<T>
                   ?? throw new UnexpectedException();
        }

        private static void AddPropertyAccessor<T>() where T : class
        {
            var property = Properties.SingleOrDefault(prop => prop.PropertyType == typeof(DbSet<T>))
                           ?? throw new UnexpectedException();

            SetGetters[typeof(T)] = context => property.GetValue(context);
        }
    }
}
