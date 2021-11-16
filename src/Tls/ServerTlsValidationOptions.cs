using System;
using System.Security.Cryptography.X509Certificates;

namespace MongoDB.Driver.Tls
{
    public class ServerTlsValidationOptions
    {
        public X509Certificate LocalCertificate { get; set; }
        public Func<ServerTlsValidationArguments, bool> ValidationCallback { get; set; }
    }
}
