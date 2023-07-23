using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

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

var consumer = new EventingBasicConsumer(channel);
consumer.Received += (sender, ea) =>
{
    var body = ea.Body.ToArray();
    var message = Encoding.UTF8.GetString(body);
    Console.WriteLine($"Received message: {message}");
    // Add your custom message handling logic here
};

channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);

Console.WriteLine("Press any key to exit.");
Console.ReadLine();

public static class RabbitMqClientSingleton
{
    //Here host name will start with b and end with .com . not requried any add any ports at the end.
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