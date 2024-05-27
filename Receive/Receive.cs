using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

var factory = new ConnectionFactory() { HostName = "localhost" };

using (var connection = factory.CreateConnection())
{
    //establecer canal
    using (var chanel = connection.CreateModel())
    {
        /*
        Acá adentro del canal, es donde ocurre todo.
        Acá creamos las colas
        */
        chanel.QueueDeclare(queue: "hola", durable: false,
            exclusive: false, autoDelete: false, arguments: null);

        var consumer = new EventingBasicConsumer(chanel);

        consumer.Received += (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);

            Console.WriteLine($"[x] Received {message}");
        };

        chanel.BasicConsume(queue: "hola", autoAck: true, consumer: consumer);

        Console.WriteLine($"Press any key to exit ...");


        Console.ReadLine();
    }



}