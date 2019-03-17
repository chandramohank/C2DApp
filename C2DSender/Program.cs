using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.Devices;
using Microsoft.Azure.Devices.Client;
namespace C2Dapp
{
    class Program
    {
        static ServiceClient serviceClient;
        static DeviceClient s_deviceClient;
        static string eventHubName = "iothubiothub-ehub-sampleappi-1381594-8565204f3d";
        static string connectionString = "HostName=iottesthub2.azure-devices.net;DeviceId=DEVICE0001;SharedAccessKey=oxBl9hRKJbw+R2yzq+LhW9CoI0RU03xAmRiGg3wyyoI=";
        static string s_connectionString = "HostName=sampleappiotHub.azure-devices.net;DeviceId=DEVICE001;SharedAccessKey=hNs84KwQmPbjSAdQLLK6y3hRCU6F6tGLVL5CcCotUPs=";
        static void Main(string[] args)
        {

            serviceClient = ServiceClient.CreateFromConnectionString(connectionString);
            s_deviceClient = DeviceClient.CreateFromConnectionString(connectionString);
            // s_deviceClient.SendEventAsync(message).Wait();
            //SendingRandomMessages();
            SendCloudToDeviceMessageAsync().Wait();
            Console.ReadLine();

        }

        private static async Task SendCloudToDeviceMessageAsync()
        {
            //var eventHubClient = EventHubClient.CreateFromConnectionString(s_connectionString);
            Console.WriteLine("Enter Message");
            while (true)
            {
                var mess = Console.ReadLine();
                if (mess=="Exit")
                {
                    break;
                }

                try
                {
                    var message = new Microsoft.Azure.Devices.Message(Encoding.ASCII.GetBytes(mess ?? throw new InvalidOperationException()));
                    Console.WriteLine("{0} > Sending message: {1}", DateTime.Now, message);
                    await serviceClient.SendAsync("DEVICE0001", message);
                }
                catch (Exception exception)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("{0} > Exception: {1}", DateTime.Now, exception.Message);
                    Console.ResetColor();
                }

                Thread.Sleep(200);
            }
        }

    }
}
