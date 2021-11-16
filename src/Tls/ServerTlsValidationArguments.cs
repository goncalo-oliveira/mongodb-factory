using System;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Extensions.Logging;

namespace MongoDB.Driver.Tls
{
    public class ServerTlsValidationArguments
    {
        public ILogger Logger { get; set; }
        public object Sender { get; set; }
        public X509Certificate LocalCertificate { get; set; }
        public X509Certificate ServerCertificate { get; set; }
        public X509Chain Chain { get; set; }
        public SslPolicyErrors SslPolicyErrors { get; set; }
    }
}
