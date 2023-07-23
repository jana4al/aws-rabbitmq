using System.Text;
using RabbitMQ.Client;

Console.WriteLine("Hello RabbitMQ... Let's play with you... :)");

var instance = RabbitMqClientSingleton.Instance;

using var connection = instance.CreateConnection();
using var channel = connection.CreateModel();
var queueName = "jana-first-queue";

channel.QueueDeclare(
    queue: queueName,
    durable: false,
    exclusive: false,
    arguments: null
);

var message = "This is my another message";
var encodedMessage = Encoding.UTF8.GetBytes(message);
channel.BasicPublish("", queueName, null, encodedMessage);
Console.WriteLine($"Message is published and the message is {message}");

public static class RabbitMqClientSingleton
{
    private static readonly string hostname = "b-***.amazonaws.com";
    private static readonly Lazy<ConnectionFactory> _instance = new(() => new ConnectionFactory()
    {
        HostName = hostname,
        UserName = "***",
        Password = "***",
        Port = 5671,
        VirtualHost = "/",
        Ssl = new SslOption() { Enabled = true, ServerName = hostname }
    });

    public static ConnectionFactory Instance => _instance.Value;
}