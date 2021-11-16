using System;
using Microsoft.Extensions.DependencyInjection.Extensions;
using MongoDB.Driver;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class MongoFactoryServiceExtensions
    {
        public static IServiceCollection AddMongoClient( this IServiceCollection services, string connectionString )
            => AddMongoClient( services, MongoFactory.DefaultNamedSettings, connectionString );

        public static IServiceCollection AddMongoClient( this IServiceCollection services, Action<MongoClientSettings> configure )
            => AddMongoClient( services, MongoFactory.DefaultNamedSettings, configure );

        public static IServiceCollection AddMongoClient( this IServiceCollection services, string name, string connectionString )
        {
            var settings = MongoClientSettings.FromConnectionString( connectionString );

            return AddMongoClient( services, name, options =>
            {
                options.CopyFrom( settings );
            } );
        }

        public static IServiceCollection AddMongoClient( this IServiceCollection services, string name, Action<MongoClientSettings> configure )
        {
            services.TryAddSingleton<IMongoFactory, MongoFactory>();
            services.Configure<MongoClientSettings>( name, configure );

            return ( services );
        }
    }
}
