namespace JpProject.Domain.Tests
{
    public class IdentityHelpers
    {
        public static string[] Providers =
        {
            "facebook",
            "google"
        };

        public static string[] Scopes =
        {
            "openid",
            "profile",
            "email",
            "username",
            "roles",
        };

        public static string[] Claims =
        {
            "sub",
            "name",
            "given_name",
            "family_name",
            "middle_name",
            "nickname",
            "preferred_username",
            "profile",
            "picture",
            "website",
            "email",
            "email_verified",
            "gender",
            "birthdate",
            "zoneinfo",
            "locale",
            "phone_number",
            "phone_number_verified",
            "address",
            "updated_at"
        };
        public static string[] Grantypes =
        {
            "implicit",
            "client_credentials",
            "authorization_code",
            "hybrid",
            "password",
            "urn:ietf:params:oauth:grant-type:device_code"
        };

        public static string[] SecretTypes =
        {
            "SharedSecret",
            "X509Thumbprint",
            "X509Name",
            "X509CertificateBase64"
        };
    }
}
