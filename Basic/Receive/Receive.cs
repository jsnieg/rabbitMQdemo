// Receiver
// Listening for messages from RabbitMQ
// Runs continuously to listen for messages and print them out.

using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

ConnectionFactory factory = new ConnectionFactory{ HostName = "localhost" };
using var connection = await factory.CreateConnectionAsync();
using var channel = await connection.CreateChannelAsync();

// Declare queue here as well, we might start the consumer before the publisher,
// queue needs to exist before we try to consume messages from it.
await channel.QueueDeclareAsync(
    queue: "hello",
    durable: false,
    exclusive: false,
    autoDelete: false,
    arguments: null
);

Console.WriteLine(" [*] Waiting for messages...");

var consumer = new AsyncEventingBasicConsumer(channel);
consumer.ReceivedAsync += (model, ea) =>
{
    var body = ea.Body.ToArray();
    var message = Encoding.UTF8.GetString(body);
    Console.WriteLine($" [x] Received {message}");
    return Task.CompletedTask;
};

await channel.BasicConsumeAsync(
    "hello",
    autoAck: true,
    consumer: consumer
);

Console.WriteLine(" Press [enter] to exit.");
Console.ReadLine();