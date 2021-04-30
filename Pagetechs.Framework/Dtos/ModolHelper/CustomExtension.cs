using System;
using System.Linq;
using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace Pagetechs.Framework.Dtos.ModolHelper
{
    public static class CustomExtension
    {
        public static IQueryable Query(this DbContext context, string entityName)
        {
            var entitis = context.Model.GetEntityTypes();
            var curtype = entitis.FirstOrDefault(x => x.DisplayName() == entityName).ClrType;
            //var type = context.Model.FindEntityType("CourseChapter").ClrType;
            return context.Query(curtype);
        }


        static readonly MethodInfo SetMethod = typeof(DbContext).GetMethod(nameof(DbContext.Set));

        public static IQueryable Query(this DbContext context, Type entityType)
        {
            var type = typeof(DbContext);
            var methods = type.GetMethods();
            var targetMethod = methods.FirstOrDefault(x => x.Name.Contains(nameof(DbContext.Set)) && x.GetParameters().Length == 0);
            //var SetMethod = type.GetMethod(nameof(DbContext.Set));
            return (IQueryable)targetMethod.MakeGenericMethod(entityType).Invoke(context, null);
        }


        //public static IQueryable Query(this DbContext context, Type entityType) =>
        //    (IQueryable)((IDbSetCache)context).GetOrAddSet(context.GetDependencies().SetSource, entityType); 

    }
}
