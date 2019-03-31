using System.IO;
using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Jp.Infra.CrossCutting.IdentityServer.Configuration
{
    /// <summary>
    /// Impl of adding a signin key for identity server 4,
    /// with an appsetting.json configuration look similar to:
    /// "SigninKeyCredentials": {
    ///     "KeyType": "KeyFile",
    ///     "FileName": "C:\\certificates\\idsv4.pfx",
    ///     "KeyStorePath": ""
    /// }
    /// </summary>
    public static class SigninCredentialExtension
    {
        private const string File = nameof(File);
        private const string Store = nameof(Store);
        private const string Temporary = nameof(Temporary);
        private const string Environment = nameof(Environment);

        private const string FileName = nameof(FileName);
        private const string FilePassword = nameof(FilePassword);
        private const string CertificateThumbprint = nameof(CertificateThumbprint);

        public static IIdentityServerBuilder AddSigninCredentialFromConfig(
            this IIdentityServerBuilder builder, IConfigurationSection options, ILogger logger, IHostingEnvironment env)
        {
            string keyType = options.GetValue<string>("Type");
            logger.LogInformation($"SigninCredentialExtension keyType is {keyType}");

            switch (keyType)
            {
                case Temporary:
                    builder.AddDeveloperSigningCredential(false);
                    break;

                case File:
                    AddCertificateFromFile(builder, options, logger, env);
                    break;

                case Store:
                    AddCertificateFromStore(builder, options, logger);
                    break;

                case Environment:
                    AddCertificateFromEnvironment(builder, logger);
                    break;
            }
            logger.LogInformation($"SigninCredentialExtension added");
            return builder;
        }

        private static void AddCertificateFromEnvironment(IIdentityServerBuilder builder, ILogger logger)
        {
            logger.LogInformation("Taking Certificate from Environment");
            var file = System.Environment.GetEnvironmentVariable("ASPNETCORE_Kestrel__Certificates__Default__Path");
            var password = System.Environment.GetEnvironmentVariable("ASPNETCORE_Kestrel__Certificates__Default__Password");
            if (System.IO.File.Exists(file))
            {
                logger.LogInformation("Taked Certificate from Environment");
                builder.AddSigningCredential(new X509Certificate2(file, password, X509KeyStorageFlags.MachineKeySet));
            }
            else
            {
                logger.LogError($"SigninCredentialExtension cannot find key file {System.Environment.GetEnvironmentVariable("ASPNETCORE_Kestrel__Certificates__Default__Path")}");
            }
        }

        private static void AddCertificateFromStore(IIdentityServerBuilder builder,
            IConfigurationSection options, ILogger logger)
        {
            var keyIssuer = options.GetValue<string>(CertificateThumbprint);
            logger.LogInformation($"SigninCredentialExtension adding key from store by {keyIssuer}");

            X509Store store = new X509Store(StoreName.My, StoreLocation.CurrentUser);
            store.Open(OpenFlags.ReadOnly);

            var certificates = store.Certificates.Find(X509FindType.FindByThumbprint, keyIssuer, false);
            logger.LogInformation($"Certificates on store: {store.Certificates.Count}");
            foreach (var storeCertificate in store.Certificates)
            {
                logger.LogInformation($"{storeCertificate.Thumbprint} - {storeCertificate.IssuerName.Name}");
            }
            if (certificates.Count > 0)
                builder.AddSigningCredential(certificates[0]);
            else
                logger.LogError("A matching key couldn't be found in the store");
        }

        private static void AddCertificateFromFile(IIdentityServerBuilder builder,
            IConfigurationSection options, ILogger logger, IHostingEnvironment env)
        {
            var keyFileName = options.GetValue<string>(FileName);
            var keyFilePassword = options.GetValue<string>(FilePassword);
            var file = Path.Combine(env.ContentRootPath, keyFileName);

            if (System.IO.File.Exists(file))
            {
                logger.LogInformation($"SigninCredentialExtension adding key from file {keyFileName}");
                builder.AddSigningCredential(new X509Certificate2(file, keyFilePassword));
            }
            else
            {
                logger.LogError($"SigninCredentialExtension cannot find key file {keyFileName}");
            }
        }
    }
}