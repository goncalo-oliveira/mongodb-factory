using System;

namespace MongoDB.Driver
{
    public static class MongoClientSettingsExtensions
    {
        /// <summary>
        /// Copies property values from a connection string
        /// </summary>
        public static void CopyFromConnectionString( this MongoClientSettings settings, string connectionString )
            => CopyFromUrl( settings, new MongoUrl( connectionString ) );

        /// <summary>
        /// Copies property values from a MongoUrl
        /// </summary>
        public static void CopyFromUrl( this MongoClientSettings settings, MongoUrl url )
            => settings.CopyFrom( MongoClientSettings.FromUrl( url ) );
    }
}
