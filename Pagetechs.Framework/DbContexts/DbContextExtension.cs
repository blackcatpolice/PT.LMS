using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using Pomelo.EntityFrameworkCore.MySql.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pagetechs.Framework.DbContexts
{
    public static class DbConfigurationExtension
    {
        public static IServiceCollection UseDbConfig<T>(this IServiceCollection services)
             where T : DbContext
        {
            var configuration = services.BuildServiceProvider().GetService<IConfiguration>();
            var dataType = configuration["Infrastructure:Database:Use"];
            var dbConection = "";
            var dbConectionKey = "default";
            switch (dataType)
            {
                case "MssqlDB":
                    dbConection = configuration[$"Infrastructure:Database:sqlserver{":" + dbConectionKey}"];
                    break;
                case "MysqlDB":
                    dbConection = configuration[$"Infrastructure:Database:mysql{":" + dbConectionKey}"];
                    break;
                case "Sqlite":
                default:
                    dbConection = configuration[$"Infrastructure:Database:sqlite{":" + dbConectionKey}"];
                    break;
            }


            switch (dataType)
            {
                case "MssqlDB":
                    services.AddDbContextPool<T>(options =>
                            options.UseSqlServer(dbConection));
                    break;
                case "MysqlDB":
                    services.AddDbContextPool<T>(options =>
                    {
                        options.UseMySql(dbConection, ServerVersion.FromString("5.7.33"), mysqlOption =>
                         {
                             mysqlOption.CharSet(CharSet.Utf8);
                         });
                    });
                    break;
                case "Sqlite":
                default:
                    services.AddDbContextPool<T>(options =>
                            options.UseSqlite(dbConection));
                    break;
            }
            return services;
        }
    }
}
