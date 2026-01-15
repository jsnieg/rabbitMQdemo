// Receiver
// Listening for messages from RabbitMQ
// Runs continuously to listen for messages and print them out.

using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Threading.Tasks;

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
consumer.ReceivedAsync += async (model, ea) =>
{
    var body = ea.Body.ToArray();
    var message = Encoding.UTF8.GetString(body);
    Console.WriteLine($" [x] Received {message}");
    int dots = message.Split(".").Length - 1;
    await Task.Delay(dots * 1000);
    Console.WriteLine(" [x] Done");

    // here channel could also be accessed as ((AsyncEventingBasicConsumer)sender)
    await channel.BasicAcksync(deliveryTag: ea.DeliveryTag, multiple: false);
};

await channel.BasicConsumeAsync(
    "hello",
    autoAck: false,
    consumer: consumer
);

Console.WriteLine(" Press [enter] to exit.");
Console.ReadLine();