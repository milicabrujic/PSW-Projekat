using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.Extensions.Hosting;

using System.Threading;
using System.Timers;
using GrpcServiceHospital;
using System.Net.Http;
using Grpc.Net.Client;

namespace GrpcServiceHospital.Services
{
    public class ClientScheduledService : IHostedService
    {
        private System.Timers.Timer _timer;
        private GrpcChannel _channel;
      //  private SpringGrpcService.SpringGrpcServiceClient _client;

        public ClientScheduledService() { }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _channel = GrpcChannel.ForAddress("https://localhost:8787", new GrpcChannelOptions() { HttpClient = CreateHttpClient() });
         //   _client = new SpringGrpcService.SpringGrpcServiceClient(_channel);
            SetUpTimer();
            return Task.CompletedTask;
        }

        private async void SendMessage(object source, ElapsedEventArgs e)
        {
            try
            {
            //    MessageResponseProto response = await _client.communicateAsync(new MessageProto() { Message = "Random message from asp.net client: " + Guid.NewGuid().ToString(), RandomInteger = new Random().Next(1, 101) });
             //   Console.WriteLine(response.Response + " is response; status: " + response.Status);
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.StackTrace);
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _channel?.ShutdownAsync();
            _timer?.Dispose();
            return Task.CompletedTask;
        }

        private HttpClient CreateHttpClient()
        {
            SetAppContextSwitch();
            var httpClient = new HttpClient(CreateHttpClientHandler());
            return httpClient;
        }

        private void SetAppContextSwitch()
        {
            AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
        }

        private HttpClientHandler CreateHttpClientHandler()
        {
            var httpHandler = new HttpClientHandler();
            httpHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
            return httpHandler;
        }

        private void SetUpTimer()
        {
            _timer = new System.Timers.Timer();
            _timer.Elapsed += new ElapsedEventHandler(SendMessage);
            _timer.Interval = 10000;
            _timer.Enabled = true;
        }
    }
}
