using System.Text;
using RabbitMQ.Client;

Console.WriteLine("Hello RabbitMQ... Let's play with you... :)");

var hostname = "b-*******.amazonaws.com";
var factory = new ConnectionFactory()
{
    HostName = hostname,
    UserName = "***",
    Password = "****",
    Port = 5671,
    VirtualHost = "/",
    Ssl = new SslOption() { Enabled = true, ServerName = hostname }
};


using var connection = factory.CreateConnection();
using var channel = connection.CreateModel();
var queueName = "jana-first-queue";

channel.QueueDeclare(
    queue: queueName,
    durable: false,
    exclusive: false,
    arguments: null
);

var message = "This is my first message";
var encodedMessage = Encoding.UTF8.GetBytes(message);
channel.BasicPublish("", queueName, null, encodedMessage);
Console.WriteLine($"Message is published and the message is {message}");