using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.Devices;
using Microsoft.Azure.Devices.Client;

namespace Receiver
{
    class Program
    {
        static ServiceClient serviceClient;
        static DeviceClient s_deviceClient;
        static string eventHubName = "iothubiothub-ehub-sampleappi-1381594-8565204f3d";

        static string connectionString =
                "HostName=iottesthub2.azure-devices.net;DeviceId=DEVICE0001;SharedAccessKey=oxBl9hRKJbw+R2yzq+LhW9CoI0RU03xAmRiGg3wyyoI="
            ;

        static string s_connectionString =
                "HostName=sampleappiotHub.azure-devices.net;DeviceId=DEVICE001;SharedAccessKey=hNs84KwQmPbjSAdQLLK6y3hRCU6F6tGLVL5CcCotUPs="
            ;

        static void Main(string[] args)
        {

            serviceClient = ServiceClient.CreateFromConnectionString(connectionString);
            s_deviceClient = DeviceClient.CreateFromConnectionString(connectionString);
            // s_deviceClient.SendEventAsync(message).Wait();
            //SendingRandomMessages();
            ReceiveC2DAsync().Wait();
            Console.ReadLine();

        }

        private static async Task ReceiveC2DAsync()
        {
            //string mockedJsonData =

            //     "{ \"Locked\":true}";
            Console.WriteLine("\nReceiving cloud to device messages from service");
            while (true)
            {
                try
                {
                    Microsoft.Azure.Devices.Client.Message receivedMessage = await s_deviceClient.ReceiveAsync();
                    //  var receivedMessage = new
                    //  Microsoft.Azure.Devices.Client.Message(Encoding.ASCII.GetBytes("Cloud to device message."));
                    if (receivedMessage == null) continue;

                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Received message: {0}",
                        Encoding.ASCII.GetString(receivedMessage.GetBytes()));
                    Console.ResetColor();

                    await s_deviceClient.CompleteAsync(receivedMessage);
                }
                catch (Exception exception)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("{0} > Exception: {1}", DateTime.Now, exception.Message);
                    Console.ResetColor();
                }
            }
        }
    }
}
