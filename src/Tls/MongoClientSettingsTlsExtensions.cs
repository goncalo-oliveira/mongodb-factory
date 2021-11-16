using System;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using MongoDB.Driver.Tls;

namespace MongoDB.Driver
{
    public static class MongoClientSettingsTlsExtensions
    {
        public static void AddServerTlsValidation( this MongoClientSettings settings, Action<ServerTlsValidationOptions> configure )
        {
            settings.UseTls = true;
            settings.AllowInsecureTls = false;

            var options = new ServerTlsValidationOptions();

            configure.Invoke( options );

            if ( options.ValidationCallback == null )
            {
                options.ValidationCallback = ServerCertificateValidation;
            }

            settings.SslSettings.ServerCertificateValidationCallback = ( sender, serverCertificate, chain, policyErrors )
                => options.ValidationCallback.Invoke( new ServerTlsValidationArguments
                {
                    Sender = sender,
                    LocalCertificate = options.LocalCertificate,
                    ServerCertificate = serverCertificate,
                    Chain = chain,
                    SslPolicyErrors = policyErrors
                } );
        }

        private static bool ServerCertificateValidation( ServerTlsValidationArguments validationArguments )
        {
            switch( validationArguments.SslPolicyErrors )
            {
                case SslPolicyErrors.None:
                {
                    return ( true );
                }
                case SslPolicyErrors.RemoteCertificateChainErrors:
                {
                    // It is a self-signed certificate, so chain length will be 1.
                    if ( validationArguments.Chain.ChainElements.Count == 1 && validationArguments.Chain.ChainStatus[0].Status == X509ChainStatusFlags.UntrustedRoot )
                    {
                        // This verifies that the issuer and serial number matches.
                        // could also use a cryptographic hash, or match the two certificates byte by byte.
                        if ( validationArguments.LocalCertificate.Equals( validationArguments.ServerCertificate ) )
                        {
                            return ( true );
                        }
                    }
                    break;
                }
                default:
                {
                    break;
                }
            }

            System.Diagnostics.Trace.TraceWarning( "Certificates don't match or remote certificate is not available." );
            
            return ( false );
        }
    }
}
