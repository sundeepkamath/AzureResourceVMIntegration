using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System;
using System.Configuration;

namespace AzureResourceVMIntegration
{
    public class AzureAD
    {
        public static string Authenticate()
        {
            AuthenticationContext authContext = new AuthenticationContext(
                                                    string.Format(ConfigurationManager.AppSettings[Constants.OAuthUrl],
                                                     ConfigurationManager.AppSettings[Constants.TenantId]), true);
            AuthenticationResult authResult = null;

            bool retry = false;
            int retryCount = 0;

            do
            {
                try
                {
                    authResult = authContext
                                    .AcquireToken(ConfigurationManager.AppSettings[Constants.ServiceResourceId],
                                                  ConfigurationManager.AppSettings[Constants.ClientId],
                                                  new UserCredential(ConfigurationManager.AppSettings[Constants.Username],
                                                  ConfigurationManager.AppSettings[Constants.Password]));

                }
                catch (AdalException adEx)
                {
                    if (adEx.ErrorCode == "temporarily_unavailable")
                    {
                        retry = true;
                        retryCount++;
                    }
                    throw;
                }
            } while ((retry == true) && (retryCount < 3));

            if (authResult == null)
            {
                Console.WriteLine("Cancelling attempt.....");
                return null;
            }

            Console.WriteLine("Authenticated successfully making HTTPS call.");
            return authResult.AccessToken;
        }
    }
}
