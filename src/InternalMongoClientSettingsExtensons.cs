using System;

namespace MongoDB.Driver
{
    internal static class InternalMongoClientSettingsExtensions
    {
        /// <summary>
        /// Copies property values from another instance
        /// </summary>
        public static void CopyFrom( this MongoClientSettings settings, MongoClientSettings other )
        {
            var properties = typeof( MongoClientSettings ).GetProperties();

            foreach ( var property in properties )
            {
                if ( !property.CanWrite )
                {
                    continue;
                }

                var value = property.GetValue( other );

                if ( value != null )
                {
                    property.SetValue( settings, value );
                }
            }
        }
    }
}
