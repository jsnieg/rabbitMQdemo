# Preqreuisites

- Installed Docker Desktop and successfully working.

- Running RabbitMQ on default port 5672 and hostname `localhost` then the cmd is `sudo docker run -it --rm --name rabbitmq -p 5672:5672 -p 15672:15672 rabbitmq:4-management` and is from `https://www.rabbitmq.com/docs/download`.

- Open `two` terminals.

- Terminal 1: `cd Receive` and `dotnet run`
- Terminal 2: `cd Send` and `dotnet run`

After a while you should see `Sent Hello, World` on `Send` to `Receiver` and it should show up on the `Receiver`.