// Sender
using RabbitMQ.Client;
using System.Text;

ConnectionFactory factory = new ConnectionFactory { HostName = "localhost" };
using var connection = await factory.CreateConnectionAsync();
using var channel = await connection.CreateChannelAsync();

await channel.QueueDeclareAsync(
    queue: "task_queue",
    durable: true,
    exclusive: false,
    autoDelete: false,
    arguments: null
);

// Declaring a queue is "idempotent (?)" - it will be created if it doesn't exist already.
// Message is byte array
var message = GetMessage(args);
var body = Encoding.UTF8.GetBytes(message);

var properties = new BasicProperties
{
    Persistent = true
};

// await channel.QueueDeclareAsync(
//     queue: "hello",
//     durable: false,
//     exclusive: false,
//     autoDelete: false,
//     arguments: null
// );

await channel.BasicPublishAsync(
    exchange: string.Empty,
    routingKey: "hello",
    body: body
);

Console.WriteLine($" [x] Sent {message}");

Console.WriteLine(" Press [enter] to exit.");
Console.ReadLine();

static string GetMessage(string[] args)
{
    return (args.Length > 0) ? string.Join(" ", args) : "Hello, World!";
}