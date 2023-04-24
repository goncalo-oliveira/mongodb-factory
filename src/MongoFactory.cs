using System;
using System.Collections.Concurrent;
using Microsoft.Extensions.Options;

namespace MongoDB.Driver
{
    internal class MongoFactory : IMongoFactory
    {
        internal const string DefaultNamedSettings = "_default";
        private readonly IOptionsMonitor<MongoClientSettings> settingsAccessor;
        private readonly ConcurrentDictionary<string, IMongoClient> instances;

        public MongoFactory( IOptionsMonitor<MongoClientSettings> optionsAccessor )
        {
            settingsAccessor = optionsAccessor;
            instances = new ConcurrentDictionary<string, IMongoClient>( StringComparer.OrdinalIgnoreCase );
        }

        public IMongoClient CreateClient()
            => CreateClient( DefaultNamedSettings );

        public IMongoClient CreateClient( string name )
        {
            if ( string.IsNullOrEmpty( name ) )
            {
                name = DefaultNamedSettings;
            }

            /*
                It is recommended to store a MongoClient instance in a global place, either
                as a static variable or in an IoC container with a singleton lifetime.

                http://mongodb.github.io/mongo-csharp-driver/2.13/reference/driver/connecting/#re-use
            */
            
            return instances.GetOrAdd(
                name,
                _ => new MongoClient( settingsAccessor.Get( name ) )
            );
        }
    }
}
