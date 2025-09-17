using System.Net;
using System.Net.Mail;
using System.Text;
using System.Text.Json;
using e_commercial.DTOs.Request.User;
using e_commercial.DTOs.Response.User;
using e_commercial.Services;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RazorLight;
public class RabbitMqProducer
{
    private readonly IChannel _channel;
    public RabbitMqProducer(IChannel channel)
    {
        _channel = channel;

    }

    public async Task ProduceAsync<T>(string queueName, T message)
    {



        await _channel.QueueDeclareAsync(queue: queueName, //queue name
                                        durable: false,
                                        //Durable = true: Hàng đợi sẽ tồn tại ngay cả khi RabbitMQ bị restart.
                                        //Durable = false: Hàng đợi sẽ bị mất nếu RabbitMQ khởi động lại.
                                        exclusive: false,
                                        //Exclusive = true: Hàng đợi chỉ được sử dụng bởi kết nối hiện tại, và sẽ bị xóa khi kết nối này đóng.
                                        //Exclusive = false: Hàng đợi có thể được sử dụng bởi nhiều kết nối.
                                        autoDelete: false,
                                        //AutoDelete = true: Hàng đợi sẽ bị xóa khi không còn consumer nào đang lắng nghe (subscribed).
                                        //AutoDelete = false: Hàng đợi vẫn tồn tại ngay cả khi không có consumer nào.
                                        arguments: null);
        //Đây là nơi bạn có thể truyền thêm các thiết lập tùy chỉnh (như TTL, dead-letter exchange, max length...).

        var json = JsonSerializer.Serialize(message);
        var body = Encoding.UTF8.GetBytes(json);

        await _channel.BasicPublishAsync(exchange: "", //Tên của Exchange bạn muốn gửi message đến, Ở đây là chuỗi rỗng "", nghĩa là Default Exchange (mặc định).
                                        routingKey: queueName, //Dùng để định tuyến message đến hàng đợi (queue), Với exchange là "", thì routingKey phải trùng tên hàng đợi để message đến đúng nơi.
                                                               // mandatory: false, //true: RabbitMQ phải định tuyến được message đến ít nhất một queue.
                                                               //     Nếu không định tuyến được, nó sẽ trả lại message cho publisher thông qua sự kiện BasicReturn.
                                                               //false: Message bị bỏ đi (dropped) nếu không định tuyến được tới queue nào (và không thông báo lỗi).  
                                        body: body);

        System.Console.WriteLine("Sent: " + json);

    
    }
    
}