using System;
using System.Collections.Generic;
using Microsoft.Extensions.Options;

namespace MongoDB.Driver
{
    internal class MongoFactory : IMongoFactory
    {
        internal const string DefaultNamedSettings = "_default";
        private readonly IOptionsMonitor<MongoClientSettings> settingsAccessor;
        private readonly IDictionary<string, IMongoClient> instances;

        public MongoFactory( IOptionsMonitor<MongoClientSettings> optionsAccessor )
        {
            settingsAccessor = optionsAccessor;
            instances = new Dictionary<string, IMongoClient>( StringComparer.OrdinalIgnoreCase );
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
            
            if ( instances.TryGetValue( name, out var client ) )
            {
                return ( client );
            }

            var settings = settingsAccessor.Get( name );

            instances.Add( name, client = new MongoClient( settings ) );

            return ( client );
        }
    }
}
