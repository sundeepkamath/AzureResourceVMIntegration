using System;
using System.Configuration;

namespace AzureResourceVMIntegration
{
    class Program
    {
        static void Main(string[] args)
        {
            string accessToken = AzureAD.Authenticate();
            
            if (accessToken != null)
            {
               AzureRestClient.ExecuteRequest(accessToken, ConfigurationManager.AppSettings[Constants.ResourceVMDetailsUrl])
                    .ContinueWith((task)=> 
                    {
                        Console.WriteLine(task.Result);
                    });
                    
            }
            Console.ReadLine();
        }
    }
}
