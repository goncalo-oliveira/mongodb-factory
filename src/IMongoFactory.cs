using System;

namespace MongoDB.Driver
{
    public interface IMongoFactory
    {
        IMongoClient CreateClient();
        IMongoClient CreateClient( string name );
    }
}
