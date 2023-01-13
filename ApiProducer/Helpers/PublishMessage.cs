using RabbitMQ.Client;
using System.Text;

namespace ApiProducer.Helpers
{
    public interface IPublishMessage
    {
        Task<string> Publish(string message);
    }

    public class PublishMessage : IPublishMessage
    {
        public Task<string> Publish(string message)
        {
            var hostname = "b-51e82498-54cb-463e-b171-28fc0ed7bdd1.mq.ap-south-1.amazonaws.com";
            var factory = new ConnectionFactory()
            {
                HostName = hostname,
                UserName = "rmq-channelsmart-qa",
                Password = "F@9gSDxmSBTHKUCKhRGxgI@3byr2ZrGvbpDav4cq",
                Port = 5671,
                VirtualHost = "/",
                Ssl = new SslOption() { Enabled = true, ServerName = hostname }
            };

            factory.AutomaticRecoveryEnabled = true; //This will will take of reconnecting if connection lost in middle. Not reuired to retry manually.
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();
            var queueName = "jana-first-queue";

            channel.QueueDeclare(
                queue: queueName,
                durable: false,
                exclusive: false,
                arguments: null
            );

            var encodedMessage = Encoding.UTF8.GetBytes(message);
            channel.BasicPublish("", queueName, null, encodedMessage);

            return Task.FromResult("Message published");
        }
    }
}