using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Jp.UI.SSO.Util
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
        private const string KeyType = "KeyType";
        private const string KeyTypeKeyFile = "File";
        private const string KeyTypeKeyStore = "Store";
        private const string KeyTypeTemporary = "Temporary";
        private const string FileName = nameof(FileName);
        private const string FilePassword = nameof(FilePassword);
        private const string KeyStoreIssuer = "KeyStoreIssuer";
        private const string KeyTypeEnvironment = nameof(KeyTypeEnvironment);



        public static IIdentityServerBuilder AddSigninCredentialFromConfig(
            this IIdentityServerBuilder builder, IConfigurationSection options, ILogger logger, IHostingEnvironment env)
        {
            string keyType = options.GetValue<string>(KeyType);
            logger.LogInformation($"SigninCredentialExtension keyType is {keyType}");

            switch (keyType)
            {
                case KeyTypeTemporary:
                    logger.LogInformation($"SigninCredentialExtension adding Temporary Signing Credential");
                    builder.AddDeveloperSigningCredential(true);
                    break;

                case KeyTypeKeyFile:
                    AddCertificateFromFile(builder, options, logger, env);
                    break;

                case KeyTypeKeyStore:
                    AddCertificateFromStore(builder, options, logger);
                    break;

                case KeyTypeEnvironment:
                    AddCertificateFromEnvironment(builder, logger);
                    break;
            }

            return builder;
        }

        private static void AddCertificateFromEnvironment(IIdentityServerBuilder builder, ILogger logger)
        {
            logger.LogInformation("Take Certifate from Environment");
            var file = Environment.GetEnvironmentVariable("ASPNETCORE_Kestrel__Certificates__Default__Path");
            var password = Environment.GetEnvironmentVariable("ASPNETCORE_Kestrel__Certificates__Default__Password");
            if (File.Exists(file))
            {
                logger.LogInformation("Taked Certifate from Environment");
                builder.AddSigningCredential(new X509Certificate2(file, password));
            }
            else
            {
                logger.LogError($"SigninCredentialExtension cannot find key file {Environment.GetEnvironmentVariable("ASPNETCORE_Kestrel__Certificates__Default__Path")}");
            }

        }

        private static void AddCertificateFromStore(IIdentityServerBuilder builder,
            IConfigurationSection options, ILogger logger)
        {
            var keyIssuer = options.GetValue<string>(KeyStoreIssuer);
            logger.LogInformation($"SigninCredentialExtension adding key from store by {keyIssuer}");

            X509Store store = new X509Store(StoreName.My, StoreLocation.LocalMachine);
            store.Open(OpenFlags.ReadOnly);

            var certificates = store.Certificates.Find(X509FindType.FindByIssuerName, keyIssuer, true);

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

            if (File.Exists(Path.Combine(env.ContentRootPath, keyFileName)))
            {
                logger.LogInformation($"SigninCredentialExtension adding key from file {keyFileName}");
                builder.AddSigningCredential(new X509Certificate2(Path.Combine(env.ContentRootPath, keyFileName), keyFilePassword));
            }
            else
            {
                logger.LogError($"SigninCredentialExtension cannot find key file {keyFileName}");
            }
        }
    }
}