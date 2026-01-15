Demonstration of RabbitMQ as per docs on here -> https://www.rabbitmq.com/tutorials

# RabbitMQ Demo

## Requirements

- Installed Docker Desktop and successfully working.

- Running RabbitMQ on default port 5672 and hostname `localhost` then the cmd is `sudo docker run -it --rm --name rabbitmq -p 5672:5672 -p 15672:15672 rabbitmq:4-management` and is from `https://www.rabbitmq.com/docs/download`.

- Installed C# with .NET Framework. This repository was worked on through WSL env.

- Run `dotnet add package RabbitMQ.Client` in directory you wish to.

## Usage

- Open `two` terminals.

- Terminal 1: `cd Receive` and `dotnet run`
- Terminal 2: `cd Send` and `dotnet run`

After a while you should see `Sent Hello, World` on `Send` to `Receiver` and it should show up on the `Receiver`.

## Work Queues

`Shell 1:` run `cd Worker` -> `dotnet run`

`Shell 2:` run `cd Worker` -> `dotnet run`

Then...

When running on `Shell 1` `cd NewTask` -> `dotnet run "First message.` This will appear on first worker.

When running on  `Shell 2` `cd NewTask` -> `dotnet run "Second message.` This will appear on first worker.

You can repeat it if you'd like to.

**Reason** being that sending each message to the next consumer happens in sequence. On average, every consumer will get the same number of messages.