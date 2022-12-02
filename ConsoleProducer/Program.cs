using System.Text;
using RabbitMQ.Client;

Console.WriteLine("Hello RabbitMQ... Let's play with you... :)");

var hostname = "b-8c289b1b-5f53-44ef-bc94-fd39d396a2f9.mq.us-east-1.amazonaws.com";
var factory = new ConnectionFactory()
{
    HostName = hostname,
    UserName = "channelsmart",
    Password = "EAQKvSWRm3kDBkKT",
    Port = 5671,
    VirtualHost = "/",
    Ssl = new SslOption() { Enabled = true, ServerName = hostname }
};


using var connection = factory.CreateConnection();
using var channel = connection.CreateModel();
var queueName = "jlqueue";

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