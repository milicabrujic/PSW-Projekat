using Newtonsoft.Json;
using PSW_backend.Dtos;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSW_backend.RabbitMQ
{
    public class RabbitMQProducer
    {
        public static void Send(PrescriptionDto prescriptionDto)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "prescriptions",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                PrescriptionDto message = prescriptionDto;

                var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));

                channel.BasicPublish(exchange: "",
                                     routingKey: "prescriptions",
                                     basicProperties: null,
                                     body: body);
                System.Diagnostics.Debug.WriteLine(" [x] Sent {0}", message);
            }
        }
    }
}
