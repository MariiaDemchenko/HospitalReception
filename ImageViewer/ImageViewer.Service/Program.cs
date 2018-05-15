using System;
using System.ServiceModel;

namespace Microsoft.ServiceModel.ImageViewer
{
    internal class Program
    {
        private static void Main()
        {
            using (var serviceHost = new ServiceHost(typeof(ImageViewer)))
            {
                serviceHost.Open();
                Console.WriteLine("The service is ready.");
                Console.WriteLine("Press <ENTER> to terminate service.");
                Console.ReadLine();
            }
        }
    }
}