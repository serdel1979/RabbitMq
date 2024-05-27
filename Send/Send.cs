using RabbitMQ.Client;
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
        string message = "Hola soy morgan";

        //PARA TRANSMITIR AL CANAL, PASO EL STRING A BYTE
        var body = Encoding.UTF8.GetBytes(message);

        chanel.BasicPublish(exchange: "", routingKey: "hola", basicProperties: null, body: body);

        Console.WriteLine($"[x] Sent {message}");
    
    }

    Console.WriteLine($"Press any key to exit ...");

    Console.ReadLine();

}