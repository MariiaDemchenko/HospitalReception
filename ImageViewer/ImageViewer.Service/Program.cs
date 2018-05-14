using Microsoft.ServiceModel.ImageViewer.Contracts.ServiceContracts;
using System;
using System.ServiceModel;
using System.ServiceModel.Description;

namespace Microsoft.ServiceModel.ImageViewer
{
    public class Program
    {
        private static void Main()
        {
            var baseAddress = new Uri("http://localhost:8000/ImageViewer/Service");
            var serviceHost = new ServiceHost(typeof(Services.ImageViewer), baseAddress);
            try
            {
                serviceHost.AddServiceEndpoint(typeof(IImageViewer),
                    new BasicHttpBinding
                    {
                        MaxReceivedMessageSize = Constants.MaxRecievedMessageSize
                    }, "ImageViewer");

                var serviceMetadataBehaviour = new ServiceMetadataBehavior
                {
                    HttpGetEnabled = true
                };

                ServiceDebugBehavior debug = serviceHost.Description.Behaviors.Find<ServiceDebugBehavior>();
                if (debug == null)
                {
                    serviceHost.Description.Behaviors.Add(
                        new ServiceDebugBehavior() { IncludeExceptionDetailInFaults = true });
                }
                else
                {
                    if (!debug.IncludeExceptionDetailInFaults)
                    {
                        debug.IncludeExceptionDetailInFaults = true;
                    }
                }

                serviceHost.Description.Behaviors.Add(serviceMetadataBehaviour);

                serviceHost.Open();
                Console.WriteLine("The service is ready.");
                Console.WriteLine("Press <ENTER> to terminate service.");
                Console.WriteLine();
                Console.ReadLine();

                serviceHost.Close();
            }
            catch (CommunicationException ce)
            {
                Console.WriteLine("An exception occurred: {0}", ce.Message);
                serviceHost.Abort();
            }
        }
    }
}